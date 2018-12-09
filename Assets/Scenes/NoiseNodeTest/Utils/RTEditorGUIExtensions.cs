using UnityEditor;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Utils {
    public class RTEditorGUIExtensions {
        public static void ReadOnlyTextField(string label, string text) {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(label, GUILayout.Width(EditorGUIUtility.labelWidth - 4));
            EditorGUILayout.SelectableLabel(text, EditorStyles.textField,
            GUILayout.Height(EditorGUIUtility.singleLineHeight));
            EditorGUILayout.EndHorizontal();
        }
    }
}