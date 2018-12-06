using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Clamp")]
    public class ClampNode : TransformerNode {
        private const string ID = "NoiseClampOperator";

        public override string GetID => ID;

		public override string Title => "Clamp value";

        public override Vector2 MinSize => new Vector2(150, 10);

		[SerializeField]
		private float _min;

		[SerializeField]
		private float _max;

		protected override void DrawConfigurationGUI() {
            _min = RTEditorGUI.FloatField(new GUIContent("Min. value"), _min);
            _max = RTEditorGUI.FloatField(new GUIContent("Max. value"), _max);
		}

		protected override ModuleBase CreateModule(ModuleBase input) {
			return new Clamp(_min, _max, input);
		}
	}
}