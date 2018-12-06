using System.Collections;
using System.Threading.Tasks;
using System.Timers;
using LibNoise;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Scenes.NoiseNodeTest {
	[Node (false, "Noise/Renderer")]
    public class NoiseRendererNode : Node {
        public const string ID = "NoiseRenderer";

        public override string GetID {
            get { return ID; }
        }
        
		public override string Title { get { return "Noise renderer"; } }
        
		[ValueConnectionKnob("Input", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob inputKnob;
	    
	    [ValueConnectionKnob("Output", Direction.Out, "Texture")]
		public ValueConnectionKnob outputKnob;

		[SerializeField]
		private int _previewSize = 128;

		private ModuleBase _input;
		
		public override void NodeGUI () 
		{
			inputKnob.DisplayLayout();
			outputKnob.DisplayLayout();
			
			_previewSize = RTEditorGUI.IntField(new GUIContent("Texture size"), _previewSize);

			if (GUI.changed) {
				NodeEditor.curNodeCanvas.OnNodeChange(this);
			}
		}

		public override bool Calculate() {
            _input = inputKnob.GetValue<ModuleBase>();
			var output = GenerateTexture();
            outputKnob.SetValue(output);
            return true;
        }

		private Texture2D GenerateTexture() {
			if (_input == null) {
				return null;
			}
            var noiseRenderer = new Noise2D(_previewSize, _input);
            noiseRenderer.GeneratePlanar(-1, 1, -1, 1);
            return noiseRenderer.GetTexture();
		}
    }
}