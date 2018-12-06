using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Subtract")]
    public class SubtractNode : CombinerNode {
        private const string ID = "NoiseSubtractOperator";

        public override string GetID => ID;

		public override string Title => "Subtract Noise";

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			var lhs = inputs[0];
			var rhs = inputs[1];
			return new Subtract(lhs, rhs);
		}
	}
}