using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.TextureNodes {
    [Node(false, "Texture/Sampler")]
    public class SamplerNode : ShaderBlitNode {
        public override string GetID => ID;

        public override string Title => "Sampler";

        private const string ID = "SampleTextureNode";

        [ValueConnectionKnob("Sample from", Direction.In, typeof(Texture))]
		public ValueConnectionKnob samplerSourceInputKnob;

        protected override void ConfigureMaterial(Material material) {
            var samplerSource = samplerSourceInputKnob.GetValue<Texture>();
            if (samplerSource == null) {
                return;
            }
            material.SetTexture("_SampleFrom", samplerSource);
        }

        protected override Shader GetShader() {
            return Shader.Find("Hidden/NodeEditor/Texture/SamplerNodeShader");
        }
    }
}