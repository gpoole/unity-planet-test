using LibNoise;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Renderer {
    public abstract class NoiseRendererNode : ConfigurableNode {
		[ValueConnectionKnob("Input", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob inputKnob;
	    
	    [ValueConnectionKnob("Output", Direction.Out, typeof(Texture))]
		public ValueConnectionKnob outputKnob;

		[SerializeField]
		private int _previewSize = 128;

		private ModuleBase _input;
	
		protected override void DrawConfigurationGUI() {
			_previewSize = RTEditorGUI.IntField(new GUIContent("Texture size"), _previewSize);
		}

		public override bool Calculate() {
            _input = inputKnob.GetValue<ModuleBase>();
			var output = GenerateTexture();
            outputKnob.SetValue<Texture>(output);
            return true;
        }

		private Texture2D GenerateTexture() {
			if (_input == null) {
				return null;
			}
            var noiseRenderer = new Noise2D(_previewSize, _input);
			ConfigureRenderer(noiseRenderer);
            return noiseRenderer.GetTexture();
		}

		protected abstract void ConfigureRenderer(Noise2D renderer);
	}
}