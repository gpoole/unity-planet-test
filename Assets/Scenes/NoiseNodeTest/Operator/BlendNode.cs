using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Blend")]
    public class BlendNode : CombinerNode {
        private const string ID = "NoiseBlendOperator";

        public override string GetID => ID;

		public override string Title => "Blend Noise";

		protected override int MinimumInputs => 3;

        [ValueConnectionKnob("Controller", Direction.In, typeof(ModuleBase))]
        public ValueConnectionKnob controllerInputKnob;

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			var lhs = inputs[1];
			var rhs = inputs[2];
			var controller = inputs[0];
			return new Blend(lhs, rhs, controller);
		}
	}
}