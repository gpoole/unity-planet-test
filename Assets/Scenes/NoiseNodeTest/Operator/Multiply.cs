using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operation/Multiply")]
    public class MultiplyNode : CombinerNode {
        private const string ID = "NoiseMultiplyOperator";

        public override string GetID => ID;

		public override string Title => "Min value";

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			var lhs = inputs[0];
			var rhs = inputs[1];
			return new Multiply(lhs, rhs);
		}
	}
}