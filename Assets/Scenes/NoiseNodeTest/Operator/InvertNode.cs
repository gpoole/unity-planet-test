using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Invert")]
    public class InvertNode : TransformerNode {
        private const string ID = "NoiseInvertOperator";

        public override string GetID => ID;

		public override string Title => "Invert value";

		protected override ModuleBase CreateModule(ModuleBase input) {
			return new Invert(input);
		}
	}
}