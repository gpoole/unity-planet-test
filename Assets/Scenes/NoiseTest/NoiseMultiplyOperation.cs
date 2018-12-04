using LibNoise;
using LibNoise.Operator;
using Scenes.NoiseGeneratorTest;

namespace UnityEngine {
    public class NoiseMultiplyOperation : ConnectedNoiseNode {
        [SerializeField]
        private NoiseNode _left;

        [SerializeField]
        private NoiseNode _right;

        private ModuleBase _operation;
        
        public override ModuleBase GetResult() {
            return new Multiply(_left.GetResult(), _right.GetResult());
        }

        protected override void OnDrawGizmosSelected() {
            DrawConnection(_left, Color.blue);
            DrawConnection(_right, Color.green);
        }
    }
}