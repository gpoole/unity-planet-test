using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operation/Exponent")]
    public class ExponentNode : TransformerNode {
        private const string ID = "NoiseExponentOperator";

        public override string GetID => ID;

		public override string Title => "Exponent";

        public override Vector2 MinSize => new Vector2(150, 10);

        public override bool AutoLayout => true;

		[SerializeField]
		private float _exponent;

		protected override void DrawConfigurationGUI() {
            _exponent = RTEditorGUI.FloatField(new GUIContent("Exponent value"), _exponent);
		}

		protected override ModuleBase CreateModule(ModuleBase input) {
			return new Exponent(_exponent, input);
		}
	}
}