using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.TextureNodes {
    public abstract class ShaderBlitNode : ConfigurableNode {
		[ValueConnectionKnob("Input", Direction.In, typeof(Texture))]
		public ValueConnectionKnob inputKnob;
        
		[ValueConnectionKnob("Output", Direction.Out, typeof(Texture))]
		public ValueConnectionKnob outputKnob;

	    [SerializeField]
	    private Shader _shader;

		public override bool AutoLayout => true;

        public override Vector2 MinSize => new Vector2(150, 10);

	    protected override void DrawConfigurationGUI() {
	        _shader = RTEditorGUI.ObjectField(new GUIContent("Shader"), _shader, false);
        }

	    public override bool Calculate() {
		    var input = inputKnob.GetValue<Texture>();
		    if (!_shader || !input || !outputKnob.connected()) {
			    return false;
		    }
		    var output = CreateOutputTexture(input);
		    var material = new Material(_shader);
		    Graphics.Blit(input, output, material);
		    outputKnob.SetValue<Texture>(output);
		    return true;
	    }

	    protected virtual RenderTexture CreateOutputTexture(Texture input) {
		    return new RenderTexture(input.width, input.height, 0);
	    }
    }
}