using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operation/Power")]
    public class PowerNode : CombinerNode {
        private const string ID = "NoisePowerOperator";

        public override string GetID => ID;

		public override string Title => "Power";

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			var lhs = inputs[0];
			var rhs = inputs[1];
			return new Power(lhs, rhs);
		}
	}
}