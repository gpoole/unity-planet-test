using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.TextureNodes {
    [Node(false, "Texture/Basic shader node")]
    public class BasicShaderNode : ShaderBlitNode {
        public override string GetID => ID;

        private const string ID = "BasicShaderNode";

        [SerializeField]
        private int _outputWidth;

        [SerializeField]
        private int _outputHeight;

        [SerializeField]
        private bool _overrideInputDimensions;

        protected override void DrawConfigurationGUI() {
            base.DrawConfigurationGUI();

            _overrideInputDimensions = RTEditorGUI.Toggle(_overrideInputDimensions, "Override input dimensions?");

            if (_overrideInputDimensions) {
                _outputHeight = RTEditorGUI.IntField("Output height", _outputHeight);
                _outputWidth = RTEditorGUI.IntField("Output width", _outputWidth);
            }
        }

        protected override RenderTexture CreateOutputTexture(Texture input) {
            if (!_overrideInputDimensions) {
                return base.CreateOutputTexture(input);
            }
            return new RenderTexture(_outputWidth, _outputHeight, 0);
        }
    }
}