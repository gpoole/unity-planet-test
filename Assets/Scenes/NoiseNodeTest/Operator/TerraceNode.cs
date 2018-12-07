using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
	[Node (false, "Noise/Operation/Terrace")]
    public class TerraceNode : TransformerNode {
        private const string ID = "NoiseTerraceOperator";

        public override string GetID => ID;

		public override string Title => "Terrace";

        public override Vector2 MinSize => new Vector2(150, 10);

		[SerializeField]
		private int _steps = 2;

		[SerializeField]
		private bool _inverted = false;

		[SerializeField]
		private bool _generate = false;

		protected override void DrawConfigurationGUI() {
            _generate = RTEditorGUI.Toggle(_generate, new GUIContent("Auto-generate curve?"));
			if (_generate) {
				_steps = RTEditorGUI.IntField(new GUIContent("Steps"), _steps);
			}
            _inverted = RTEditorGUI.Toggle(_inverted, new GUIContent("Invert?"));
		}

		protected override ModuleBase CreateModule(ModuleBase input) {
			var terrace = new Terrace(_inverted, input);
			if (_generate) {
                terrace.Generate(_steps);
			}
			return terrace;
		}
	}
}