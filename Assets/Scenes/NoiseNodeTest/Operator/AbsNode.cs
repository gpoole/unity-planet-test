using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Abs")]
    public class AbsNode : TransformerNode {
        private const string ID = "NoiseAbsOperator";

        public override string GetID => ID;

		public override string Title => "Absolute value";

		protected override ModuleBase CreateModule(ModuleBase input) {
			return new Abs(input);
		}
	}
}