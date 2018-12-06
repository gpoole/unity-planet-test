using LibNoise;
using LibNoise.Generator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Generator {
    [Node(false, "Noise/Generator/Constant")]
    public class ConstNode : Node {
        private const string ID = "ConstNoiseGenerator";

        public override string GetID => ID;

        public override string Title => "Constant pattern generator";

        public override Vector2 MinSize => new Vector2(200, 10);

        public override bool AutoLayout => true;

        [ValueConnectionKnob("Output", Direction.Out, typeof(ModuleBase))]
        public ValueConnectionKnob outputKnob;

        [SerializeField]
        private float _value;

        public override void NodeGUI() {
            outputKnob.DisplayLayout();

            _value = RTEditorGUI.FloatField(new GUIContent("Constant value"), _value);

            if (GUI.changed) {
                NodeEditor.curNodeCanvas.OnNodeChange(this);
            }
        }

        public override bool Calculate() {
            outputKnob.SetValue<ModuleBase>(new Const(_value));
            return true;
        }
    }
}