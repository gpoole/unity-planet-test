using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.TextureNodes {
    [Node(false, "Texture/Normal map")]
    public class NormalMapNode : ShaderBlitNode {
        public override string GetID => ID;

        public override string Title => "Normal map";

        private const string ID = "NomralMapTextureNode";

        protected override Shader GetShader() {
            return Shader.Find("Hidden/NodeEditor/Texture/HeightMapNodeShader");
        }
    }
}