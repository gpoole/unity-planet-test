using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operation/Rotate")]
    public class RotateNode : TransformerNode {
        private const string ID = "NoiseRotateOperator";

        public override string GetID => ID;

		public override string Title => "Rotate";

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
			return new Rotate(_x, _y, _z, input);
		}
	}
}