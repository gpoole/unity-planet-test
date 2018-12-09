using LibNoise;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Renderer {
    public abstract class NoiseRendererNode : ConfigurableNode {
		[ValueConnectionKnob("Input", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob inputKnob;
	    
	    [ValueConnectionKnob("Height map;", Direction.Out, typeof(Texture))]
		public ValueConnectionKnob outputKnob;

	    [ValueConnectionKnob("Normal map", Direction.Out, typeof(Texture))]
	    public ValueConnectionKnob normalMapOutputKnob;

		[SerializeField]
		private int _previewSize = 128;

		private ModuleBase _input;
	
		protected override void DrawConfigurationGUI() {
			_previewSize = RTEditorGUI.IntField(new GUIContent("Texture size"), _previewSize);
		}

		public override bool Calculate() {
            _input = inputKnob.GetValue<ModuleBase>();

            var noiseRenderer = new Noise2D(_previewSize, _input);
			ConfigureRenderer(noiseRenderer);
            
			if (outputKnob.connected()) {
				var output = noiseRenderer.GetTexture();
	            outputKnob.SetValue<Texture>(output);
			}

			if (normalMapOutputKnob.connected()) {
				var output = noiseRenderer.GetNormalMap(1);
				normalMapOutputKnob.SetValue<Texture>(output);
			}

            return true;
        }

		protected abstract void ConfigureRenderer(Noise2D renderer);
	}
}