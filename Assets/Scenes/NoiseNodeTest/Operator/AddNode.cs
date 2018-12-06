using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Add")]
    public class AddNode : CombinerNode {
        private const string ID = "NoiseAddOperator";

        public override string GetID => ID;

		public override string Title => "Add Noise";

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			var lhs = inputs[0];
			var rhs = inputs[1];
			return new Add(lhs, rhs);
		}
	}
}