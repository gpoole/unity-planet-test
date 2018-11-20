using UnityEngine;

public class PlanetShaderSetup : MonoBehaviour {
	[SerializeField]
	private Texture2D[] _textures;
	
	private void Start() {
		var material = GetComponentInChildren<Renderer>().material;
		Texture2DArray planetAtlas = new Texture2DArray(8192, 4096, _textures.Length, _textures[0].format, false);
		for (var i = 0; i < _textures.Length; i++) {
			Graphics.CopyTexture(_textures[i], 0, 0, planetAtlas, i, 0);
		}
		planetAtlas.Apply();
		material.SetTexture("_HeightMaps", planetAtlas);
	}
}
