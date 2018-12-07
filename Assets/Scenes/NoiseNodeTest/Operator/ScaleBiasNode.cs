using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operation/Scale bias")]
    public class ScaleBiasNode : TransformerNode {
        private const string ID = "NoiseScaleBiasOperator";

        public override string GetID => ID;

		public override string Title => "Scale bias";

        public override Vector2 MinSize => new Vector2(150, 10);

		[SerializeField]
		private float _scale = 1;

		[SerializeField]
		private float _bias;

		protected override void DrawConfigurationGUI() {
            _scale = RTEditorGUI.FloatField(new GUIContent("Scale"), _scale);
            _bias = RTEditorGUI.FloatField(new GUIContent("Bias"), _bias);
		}

		protected override ModuleBase CreateModule(ModuleBase input) {
			return new ScaleBias(_scale, _bias, input);
		}
	}
}