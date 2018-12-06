using LibNoise;
using LibNoise.Generator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Generator {
    [Node(false, "Noise/Generator/Spheres")]
    public class SpheresNode : Node {
        private const string ID = "SpheresNoiseGenerator";

        public override string GetID => ID;

        public override string Title => "Sphere pattern generator";

        public override Vector2 MinSize => new Vector2(200, 10);

        public override bool AutoLayout => true;

        [ValueConnectionKnob("Output", Direction.Out, typeof(ModuleBase))]
        public ValueConnectionKnob outputKnob;

        [SerializeField]
        private float _frequency;

        public override void NodeGUI() {
            outputKnob.DisplayLayout();

            _frequency = RTEditorGUI.FloatField(new GUIContent("Frequency"), _frequency);

            if (GUI.changed) {
                NodeEditor.curNodeCanvas.OnNodeChange(this);
            }
        }

        public override bool Calculate() {
            outputKnob.SetValue<ModuleBase>(new Spheres(_frequency));
            return true;
        }
    }
}