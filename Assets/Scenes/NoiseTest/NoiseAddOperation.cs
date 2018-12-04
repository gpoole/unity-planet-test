using LibNoise;
using LibNoise.Operator;
using Scenes.NoiseGeneratorTest;

namespace UnityEngine {
    public class NoiseAddOperation : ConnectedNoiseNode {
        [SerializeField]
        private NoiseNode _left;

        [SerializeField]
        private NoiseNode _right;

        private Add _operation;
        
        public override ModuleBase GetResult() {
            return new Add(_left.GetResult(), _right.GetResult());
        }

        protected override void OnDrawGizmosSelected() {
            DrawConnection(_left, Color.blue);
            DrawConnection(_right, Color.green);
        }
    }
}