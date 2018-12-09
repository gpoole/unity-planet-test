using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using Scenes.NoiseNodeTest.Utils;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Generator {
    public class SeedGUIUtils {
        public static int GetSeed(ValueConnectionKnob knob, int localValue) {
            Debug.Assert(typeof(int).IsAssignableFrom(knob.valueType), "Tried to read seed from a non-int source knob.");
            if (knob.connected()) {
                return knob.GetValue<int>();
            }

            return localValue;
        }

        public static int SeedInput(ValueConnectionKnob knob, int localValue) {
            int seed;
            if (knob.connected()) {
                seed = GetSeed(knob, localValue);
                RTEditorGUIExtensions.ReadOnlyTextField("Seed", seed.ToString());
            } else {
                seed = RTEditorGUI.IntField(new GUIContent("Seed"), localValue);
            }
            knob.SetPosition();
            return seed;
        }
    }
}