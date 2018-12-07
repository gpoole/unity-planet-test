using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.TextureNodes {
    public abstract class ShaderBlitNode : ConfigurableNode {
		[ValueConnectionKnob("Input", Direction.In, typeof(Texture))]
		public ValueConnectionKnob inputKnob;
        
		[ValueConnectionKnob("Output", Direction.Out, typeof(Texture))]
		public ValueConnectionKnob outputKnob;

		public override bool AutoLayout => true;

        public override Vector2 MinSize => new Vector2(150, 10);

        [SerializeField]
        private int _outputWidth;

        [SerializeField]
        private int _outputHeight;

        [SerializeField]
        private bool _overrideInputDimensions;

	    private Material _material;

	    protected override void DrawConfigurationGUI() {
            _overrideInputDimensions = RTEditorGUI.Toggle(_overrideInputDimensions, "Override input dimensions?");

            if (_overrideInputDimensions) {
                _outputHeight = RTEditorGUI.IntField("Output height", _outputHeight);
                _outputWidth = RTEditorGUI.IntField("Output width", _outputWidth);
            }
        }

	    public override bool Calculate() {
		    var input = inputKnob.GetValue<Texture>();
		    if (!input || !outputKnob.connected()) {
			    return true;
		    }
		    var output = CreateOutputTexture(input);
		    if (_material == null) {
				_material = new Material(GetShader());
		    }
		    ConfigureMaterial(_material);
		    Graphics.Blit(input, output, _material);
		    
            var prevActive = RenderTexture.active;
            RenderTexture.active = output;
            var outputTexture = new Texture2D(output.width, output.height);
            outputTexture.ReadPixels(new Rect(0, 0, output.width, output.height), 0, 0);
            outputTexture.Apply();
            RenderTexture.active = prevActive;

		    outputKnob.SetValue<Texture>(outputTexture);
		    return true;
	    }

	    private RenderTexture CreateOutputTexture(Texture input) {
            if (!_overrideInputDimensions) {
                return new RenderTexture(input.width, input.height, 0);
            }
            return new RenderTexture(_outputWidth, _outputHeight, 0);
	    }

	    protected virtual void ConfigureMaterial(Material material) {
	    }

	    protected abstract Shader GetShader();
    }
}