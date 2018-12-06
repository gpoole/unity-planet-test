using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Min")]
    public class MinNode : CombinerNode {
        private const string ID = "NoiseMinOperator";

        public override string GetID => ID;

		public override string Title => "Min value";

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			var lhs = inputs[0];
			var rhs = inputs[1];
			return new Min(lhs, rhs);
		}
	}
}