using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.TextureNodes {
    [Node(false, "Texture/Pass filter")]
    public class PassFilterNode : ShaderBlitNode {
        enum PassType {
            High,
            Low,
            Range,
        }
        
        public override string GetID => ID;

        public override string Title => "Pass filter";

        private const string ID = "PassFilterTextureNode";

        [SerializeField]
        private float _min;

        [SerializeField]
        private float _max;

        [SerializeField]
        private PassType _passType;

        protected override void DrawConfigurationGUI() {
            base.DrawConfigurationGUI();
            _passType = (PassType)RTEditorGUI.EnumPopup("Pass type", _passType);

            if (_passType == PassType.High || _passType == PassType.Range) {
                _min = RTEditorGUI.FloatField("Min", _min);
            }

            if (_passType == PassType.Low || _passType == PassType.Range) {
                _max = RTEditorGUI.FloatField("Max", _max);
            }
        }

        protected override void ConfigureMaterial(Material material) {
		    material.SetFloat("_PassMode", (float)_passType);
            material.SetFloat("_Min", _min);
            material.SetFloat("_Max", _max);
        }

        protected override Shader GetShader() {
            return Shader.Find("Hidden/NodeEditor/Texture/PassFilterNodeShader");
        }
    }
}