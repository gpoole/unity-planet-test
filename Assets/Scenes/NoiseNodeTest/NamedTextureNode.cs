using System.Collections;
using System.Threading.Tasks;
using System.Timers;
using LibNoise;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

namespace Scenes.NoiseNodeTest {
	[Node (false, "Texture/Assign name")]
    public class NamedTextureNode : Node {
        public const string ID = "AssignTextureName";

        public override string GetID {
            get { return ID; }
        }
        
		public override string Title { get { return "Assign texture name"; } }

		public string TextureName => _textureName;

		public Texture Texture => _texture;
        
		[ValueConnectionKnob("Input", Direction.In, "Texture")]
		public ValueConnectionKnob inputKnob;

		[SerializeField]
		private string _textureName = "Output";

		private Texture _texture;
		
		public override void NodeGUI () 
		{
			inputKnob.DisplayLayout();
			
			_textureName = RTEditorGUI.TextField(new GUIContent("Texture name"), _textureName);

			if (GUI.changed) {
				NodeEditor.curNodeCanvas.OnNodeChange(this);
			}
		}

		public override bool Calculate() {
            _texture = inputKnob.GetValue<Texture2D>();
            return true;
        }
    }
}