using System;
using UnityEngine;
using System.IO;
using NodeEditorFramework.Utilities;

namespace NodeEditorFramework.TextureComposer
{
	[Node(false, "Texture/Output")]
	public class TextureOutputNode : Node
	{
		public const string ID = "texOutNode";
		public override string GetID { get { return ID; } }

		public override string Title { get { return "Texture Output"; } }
		public override Vector2 DefaultSize { get { return new Vector2(150, 50); } }
		public override bool AutoLayout { get { return true; } }

		[ValueConnectionKnob("Texture", Direction.In, typeof(Texture))]
		public ValueConnectionKnob inputKnob;

		public string savePath = null;

		private Texture _output;

		public override void NodeGUI()
		{
			inputKnob.DisplayLayout();

			if (_output != null)
				RTTextureViz.DrawTexture(_output, 64);

			GUILayout.BeginHorizontal();
			RTEditorGUI.TextField(savePath);
#if UNITY_EDITOR
			if (GUILayout.Button("#", GUILayout.ExpandWidth (false)))
			{
				savePath = UnityEditor.EditorUtility.SaveFilePanel("Save Texture Path", Application.dataPath, "OutputTex", "png");
			}
#endif
			GUILayout.EndHorizontal();

			if (GUI.changed)
				NodeEditor.curNodeCanvas.OnNodeChange(this);
		}

		public override bool Calculate()
		{
			_output = null;

			if (!inputKnob.connected()) {
				return true;
			}
			var input = inputKnob.GetValue<Texture>();

			if (input && !string.IsNullOrEmpty(savePath))
			{
				Directory.CreateDirectory(Path.GetDirectoryName(savePath));
				Debug.Log("Saving to '" + savePath + "'!");
				Texture2D outputTexture = input as Texture2D;
				
				if (outputTexture == null && input is RenderTexture) {
					var renderTex = input as RenderTexture;
					var prevActive = RenderTexture.active;
					RenderTexture.active = renderTex;
					outputTexture = new Texture2D(input.width, input.height);
					outputTexture.ReadPixels(new Rect(0, 0, input.width, input.height), 0, 0);
					outputTexture.Apply();
					RenderTexture.active = prevActive;
				}

				if (outputTexture == null) {
					Debug.LogWarning($"Failed to generate output for {savePath}, unrecognised texture type. Should be one of: Texture2D or RenderTexture.");
					return false;
				}

                File.WriteAllBytes(savePath, outputTexture.EncodeToPNG());

#if UNITY_EDITOR
				UnityEditor.AssetDatabase.Refresh();
#endif
				_output = outputTexture;
			}
			return true;
		}
	}
}
