using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operation/Select")]
    public class SelectNode : CombinerNode {
        private const string ID = "NoiseSelectOperator";

        public override string GetID => ID;

		public override string Title => "Select Noise";

        [ValueConnectionKnob("Controller", Direction.In, typeof(ModuleBase))]
        public ValueConnectionKnob controllerInputKnob;

		protected override int MinimumInputs => 3;

		[SerializeField]
		private float _min;

		[SerializeField]
		private float _max;

		[SerializeField]
		private float _falloff;

		protected override void DrawConfigurationGUI() {
            _min = RTEditorGUI.FloatField(new GUIContent("Min."), _min);
            _max = RTEditorGUI.FloatField(new GUIContent("Max."), _max);
            _falloff = RTEditorGUI.FloatField(new GUIContent("Falloff"), _falloff);
		}

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
            var lhs = inputs[1];
            var rhs = inputs[2];
            var controller = inputs[0];
            var select = new Select(lhs, rhs, controller);
			select.Maximum = _max;
			select.Minimum = _min;
			select.FallOff = _falloff;
			return select;
		}
	}
}