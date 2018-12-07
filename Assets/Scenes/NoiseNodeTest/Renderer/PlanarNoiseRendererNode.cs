using LibNoise;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Renderer {
	[Node (false, "Noise/Renderer/Planar")]
    public class PlanarNoiseRendererNode : NoiseRendererNode {
        public const string ID = "PlanarNoiseRenderer";

        public override string GetID => ID;

		public override string Title { get { return "Planar noise renderer"; } }

		public override bool AutoLayout => true;

        public override Vector2 MinSize => new Vector2(150, 10);

		[SerializeField]
		private int _offsetLeft = -1;

		[SerializeField]
		private int _offsetTop = -1;

		[SerializeField]
		private int _offsetRight = 1;

		[SerializeField]
		private int _offsetBottom = 1;

		[SerializeField]
		private bool _isSeamless;
	
		protected override void DrawConfigurationGUI() {
			base.DrawConfigurationGUI();

			_offsetTop = RTEditorGUI.IntField(new GUIContent("Top offset"), _offsetTop);
			_offsetLeft = RTEditorGUI.IntField(new GUIContent("Left offset"), _offsetLeft);
			_offsetRight = RTEditorGUI.IntField(new GUIContent("Right offset"), _offsetRight);
			_offsetBottom = RTEditorGUI.IntField(new GUIContent("Bottom offset"), _offsetBottom);
			_isSeamless = RTEditorGUI.Toggle(_isSeamless, new GUIContent("Is seamless?"));
		}

		protected override void ConfigureRenderer(Noise2D renderer) {
            renderer.GeneratePlanar(_offsetLeft, _offsetRight, _offsetTop, _offsetBottom, _isSeamless);
		}
	}
}