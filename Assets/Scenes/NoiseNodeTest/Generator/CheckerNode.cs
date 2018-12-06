using LibNoise;
using LibNoise.Generator;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Generator {
    [Node(false, "Noise/Generator/Checker")]
    public class CheckerNode : Node {
        private const string ID = "CheckerNode";

        public override string GetID => ID;

        public override string Title => "Checker pattern generator";

        [ValueConnectionKnob("Output", Direction.Out, typeof(ModuleBase))]
        public ValueConnectionKnob outputKnob;

        public override void NodeGUI() {
            outputKnob.DisplayLayout();
        }

        public override bool Calculate() {
            outputKnob.SetValue<ModuleBase>(new Checker());
            return true;
        }
    }
}