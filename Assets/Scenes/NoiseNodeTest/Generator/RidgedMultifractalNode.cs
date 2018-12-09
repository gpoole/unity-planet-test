using LibNoise;
using LibNoise.Generator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Generator {
    [Node(false, "Noise/Generator/Ridged Multifractal")]
    public class RidgedMultifractalNode : Node {
        private const string ID = "RidgedMultifractalNoiseGenerator";

        public override string GetID => ID;

        public override string Title => "Ridged Multifractal generator";

        public override Vector2 MinSize => new Vector2(200, 10);

        public override bool AutoLayout => true;

        [ValueConnectionKnob("Output", Direction.Out, typeof(ModuleBase))]
        public ValueConnectionKnob outputKnob;

        [ValueConnectionKnob("Seed", Direction.In, typeof(int))]
        public ValueConnectionKnob _seedInputKnob;

        [SerializeField]
        private float _frequency;

        [SerializeField]
        private float _lacunarity;

        [SerializeField]
        private int _octaves;

        [SerializeField]
        private int _seed;

        [SerializeField]
        private QualityMode _quality;

        public override void NodeGUI() {
            outputKnob.DisplayLayout();

            _frequency = RTEditorGUI.FloatField(new GUIContent("Frequency"), _frequency);
            _lacunarity = RTEditorGUI.FloatField(new GUIContent("Lacunarity"), _lacunarity);
            _octaves = RTEditorGUI.IntField(new GUIContent("Octaves"), _octaves);
            _seed = SeedGUIUtils.SeedInput(_seedInputKnob, _seed);
            _quality = (QualityMode) RTEditorGUI.EnumPopup(new GUIContent("Quality"), _quality);

            if (GUI.changed) {
                NodeEditor.curNodeCanvas.OnNodeChange(this);
            }
        }

        public override bool Calculate() {
            var seed = SeedGUIUtils.GetSeed(_seedInputKnob, _seed);
            outputKnob.SetValue<ModuleBase>(new RidgedMultifractal(_frequency, _lacunarity, _octaves, seed, _quality));
            return true;
        }
    }
}