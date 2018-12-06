using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Max")]
    public class MaxNode : CombinerNode {
        private const string ID = "NoiseMaxOperator";

        public override string GetID => ID;

		public override string Title => "Max value";

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			var lhs = inputs[0];
			var rhs = inputs[1];
			return new Max(lhs, rhs);
		}
	}
}