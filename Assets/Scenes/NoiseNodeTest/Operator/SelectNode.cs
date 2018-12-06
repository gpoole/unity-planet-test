using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operations/Select")]
    public class SelectNode : CombinerNode {
        private const string ID = "NoiseSelectOperator";

        public override string GetID => ID;

		public override string Title => "Select Noise";

        [ValueConnectionKnob("Controller", Direction.In, typeof(ModuleBase))]
        public ValueConnectionKnob controllerInputKnob;

		[SerializeField]
		private float _min;

		[SerializeField]
		private float _max;

		[SerializeField]
		private float _falloff;

		protected override void DrawConfigurationGUI() {
			if (!controllerInputKnob.IsValueNull) {
	            _min = RTEditorGUI.FloatField(new GUIContent("Min."), _min);
                _max = RTEditorGUI.FloatField(new GUIContent("Max."), _max);
                _falloff = RTEditorGUI.FloatField(new GUIContent("Falloff."), _falloff);
			}
		}

		protected override ModuleBase CreateModule(ModuleBase[] inputs) {
			Debug.Assert(inputs.Length == 2 || inputs.Length == 3, "Unexpected number of inputs given");
			
			if (inputs.Length == 2) {
				var lhs = inputs[0];
				var rhs = inputs[1];
				return new Select(_min, _max, _falloff, lhs, rhs);
			}

			if (inputs.Length == 3) {
				var lhs = inputs[1];
				var rhs = inputs[2];
				var controller = inputs[0];
				return new Select(lhs, rhs, controller);
			}

			return null;
		}
	}
}