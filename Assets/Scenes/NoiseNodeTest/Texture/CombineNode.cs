using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.TextureNodes {
    [Node(false, "Texture/Combine")]
    public class CombineNode : ShaderBlitNode {
        enum CombineMode {
            Add,
            Subtract,
            Multiply,
            Divide,
        }
        
        public override string GetID => ID;

        public override string Title => "Combine textures";

        [ValueConnectionKnob("Combine with", Direction.In, typeof(Texture))]
		public ValueConnectionKnob combineInputKnob;

        private const string ID = "CombineTextureNode";
        
        [SerializeField]
        private CombineMode _combineMode;

        protected override void DrawConfigurationGUI() {
            base.DrawConfigurationGUI();
            _combineMode = (CombineMode)RTEditorGUI.EnumPopup("Combine mode", _combineMode);
        }

        protected override void ConfigureMaterial(Material material) {
            var combineTex = combineInputKnob.GetValue<Texture>();
            if (combineTex) {
                material.SetTexture("_CombineTex", combineTex);
            }
		    material.SetFloat("_CombineMode", (float)_combineMode);
        }

        protected override Shader GetShader() {
            return Shader.Find("Hidden/NodeEditor/Texture/CombineNodeShader");
        }
    }
}