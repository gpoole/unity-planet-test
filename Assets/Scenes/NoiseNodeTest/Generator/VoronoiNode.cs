using LibNoise;
using LibNoise.Generator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Generator {
    [Node(false, "Noise/Generator/Voronoi")]
    public class VoronoiNode : Node {
        private const string ID = "VoronoiNoiseGenerator";

        public override string GetID => ID;

        public override string Title => "Voronoi generator";

        public override Vector2 MinSize => new Vector2(200, 10);

        public override bool AutoLayout => true;

        [ValueConnectionKnob("Output", Direction.Out, typeof(ModuleBase))]
        public ValueConnectionKnob outputKnob;

        [SerializeField]
        private float _frequency;

        [SerializeField]
        private float _displacement;

        [SerializeField]
        private int _seed;

        [SerializeField]
        private bool _distance;

        public override void NodeGUI() {
            outputKnob.DisplayLayout();

            _frequency = RTEditorGUI.FloatField(new GUIContent("Frequency"), _frequency);
            _displacement = RTEditorGUI.FloatField(new GUIContent("Lacunarity"), _displacement);
            _seed = RTEditorGUI.IntField(new GUIContent("Seed"), _seed);
            _distance = RTEditorGUI.Toggle(_distance, new GUIContent("Distance"));

            if (GUI.changed) {
                NodeEditor.curNodeCanvas.OnNodeChange(this);
            }
        }

        public override bool Calculate() {
            outputKnob.SetValue<ModuleBase>(new Voronoi(_frequency, _displacement, _seed, _distance));
            return true;
        }
    }
}