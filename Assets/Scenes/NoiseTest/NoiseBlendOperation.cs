using LibNoise;
using LibNoise.Operator;
using Scenes.NoiseGeneratorTest;

namespace UnityEngine {
    public class NoiseBlendOperation : ConnectedNoiseNode {
        [SerializeField]
        private NoiseNode _left;

        [SerializeField]
        private NoiseNode _right;

        [SerializeField]
        private NoiseNode _controller;

        private Blend _operation;
        
        public override ModuleBase GetResult() {
            return new Blend(_left.GetResult(), _right.GetResult(), _controller.GetResult());
        }

        protected override void OnDrawGizmosSelected() {
            DrawConnection(_left, Color.blue);
            DrawConnection(_right, Color.green);
            DrawConnection(_controller, Color.cyan);
        }
    }
}