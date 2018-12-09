using System;
using LibNoise;
using LibNoise.Generator;
using NodeEditorFramework;
using Scenes.NoiseNodeTest.Utils;
using UnityEngine;
using Random = System.Random;

namespace Scenes.NoiseNodeTest.Generator {
    [Node(false, "Noise/Generator/Random seed")]
    public class RandomSeedNode : Node {
        private const string ID = "RandomSeedNode";

        public override string GetID => ID;

        public override string Title => "Random seed";

        [ValueConnectionKnob("Output", Direction.Out, typeof(int))]
        public ValueConnectionKnob outputKnob;

        [SerializeField]
        private int _value;

        protected override void OnCreate() {
            Regenerate();
        }

        public override void NodeGUI() {
            outputKnob.DisplayLayout();

            RTEditorGUIExtensions.ReadOnlyTextField("Value", _value.ToString());
            
            if (GUILayout.Button("Regenerate")) {
                Regenerate();
            }
        }

        public override bool Calculate() {
            outputKnob.SetValue(_value);
            return true;
        }

        private void Regenerate() {
            var random = new Random();
            // Should it be -max to max?
            _value = random.Next();
            NodeEditor.curNodeCanvas.OnNodeChange(this);
        }
    }
}