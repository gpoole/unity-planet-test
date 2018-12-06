using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Displace")]
    public class DisplaceNode : CombinerNode {
        private const string ID = "NoiseDisplaceOperator";

        public override string GetID => ID;

		public override string Title => "Displace noise";

		protected override int MinimumInputs => 4;

		[ValueConnectionKnob("X disp.", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob xInputKnob;

		[ValueConnectionKnob("Y disp.", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob yInputKnob;

		[ValueConnectionKnob("Z disp.", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob zInputKnob;

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			var input = inputs[3];
			var x = inputs[0];
			var y = inputs[1];
			var z = inputs[2];
			return new Displace(input, x, y, z);
		}
	}
}