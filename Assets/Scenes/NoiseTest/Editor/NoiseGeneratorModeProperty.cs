using UnityEditor;
using UnityEngine;

namespace Scenes.NoiseGeneratorTest.EditorOnly {
    [CustomPropertyDrawer(typeof(NoiseGeneratorModeAttribute))]
    public class NoiseGeneratorModeProperty : PropertyDrawer {
        private NoiseGeneratorModeAttribute ModeAttribute => attribute as NoiseGeneratorModeAttribute;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            if (IsShowing(property)) {
                EditorGUI.PropertyField(position, property, label, true);
            }
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            if (IsShowing(property)) {
                return EditorGUI.GetPropertyHeight(property, label);
            }

            return -EditorGUIUtility.standardVerticalSpacing;
        }

        private bool IsShowing(SerializedProperty property) {
            NoiseGenerator.NoiseType noiseType = (NoiseGenerator.NoiseType)property.serializedObject.FindProperty("_type").intValue;
            return (ModeAttribute.Type & noiseType) == noiseType;
        }
    }
}