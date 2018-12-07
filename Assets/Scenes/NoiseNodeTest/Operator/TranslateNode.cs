using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operation/Translate")]
    public class TranslateNode : TransformerNode {
        private const string ID = "NoiseTranslateOperator";

        public override string GetID => ID;

		public override string Title => "Translate";

        public override Vector2 MinSize => new Vector2(150, 10);

		[SerializeField]
		private float _x;

		[SerializeField]
		private float _y;

		[SerializeField]
		private float _z;

		protected override void DrawConfigurationGUI() {
            _x = RTEditorGUI.FloatField(new GUIContent("X"), _x);
            _y = RTEditorGUI.FloatField(new GUIContent("Y"), _y);
            _z = RTEditorGUI.FloatField(new GUIContent("Z"), _z);
		}

		protected override ModuleBase CreateModule(ModuleBase input) {
			return new Translate(_x, _y, _z, input);
		}
	}
}