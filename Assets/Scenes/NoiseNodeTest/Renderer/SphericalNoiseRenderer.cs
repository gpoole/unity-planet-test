using LibNoise;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Renderer {
	[Node (false, "Noise/Renderer/Spherical")]
    public class SphericalNoiseRendererNode : NoiseRendererNode {
        public const string ID = "SphericalNoiseRenderer";

        public override string GetID => ID;

		public override string Title => "Spherical noise renderer";

		public override bool AutoLayout => true;

        public override Vector2 MinSize => new Vector2(150, 10);

		[SerializeField]
		private int _offsetNorth = -90;

		[SerializeField]
		private int _offsetSouth = 90;

		[SerializeField]
		private int _offsetEast = 180;

		[SerializeField]
		private int _offsetWest = -180;
	
		protected override void DrawConfigurationGUI() {
			base.DrawConfigurationGUI();

			_offsetNorth = RTEditorGUI.IntField(new GUIContent("North offset"), _offsetNorth);
			_offsetSouth = RTEditorGUI.IntField(new GUIContent("South offset"), _offsetSouth);
			_offsetEast = RTEditorGUI.IntField(new GUIContent("East offset"), _offsetEast);
			_offsetWest = RTEditorGUI.IntField(new GUIContent("West offset"), _offsetWest);
		}

		protected override void ConfigureRenderer(Noise2D renderer) {
            renderer.GenerateSpherical(_offsetSouth, _offsetNorth, _offsetWest, _offsetEast);
		}
	}
}