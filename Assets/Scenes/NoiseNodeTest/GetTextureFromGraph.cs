using System;
using System.Collections;
using UnityEngine;

namespace Scenes.NoiseNodeTest {
    public class GetTextureFromGraph : MonoBehaviour {
        public const float UpdateInterval = 1f;
        
        public TextureNodeCanvas Canvas;

        public string OutputTextureName;

        public string TextureSlotName;

        private Material _material;

        private void Start() {
            // FIXME: should be sharedMaterial?
            _material = GetComponentInChildren<UnityEngine.Renderer>().sharedMaterial;

            if (Application.isEditor) {
                StartCoroutine(PeriodicUpdate());
            } else {
                UpdateFromGraph();
            }
        }

        private IEnumerator PeriodicUpdate() {
            var wait = new WaitForSeconds(UpdateInterval);
            while (true) {
                try {
                    UpdateFromGraph();
                } catch (Exception ex) {
                    Debug.LogError(ex);
                }
                yield return wait;
            }
        }

        private void UpdateFromGraph() {
            if (!Canvas || string.IsNullOrEmpty(OutputTextureName) || string.IsNullOrEmpty(TextureSlotName)) {
                return;
            }
            var texture = Canvas.GetTexture(OutputTextureName);
            if (texture) {
                _material.SetTexture(TextureSlotName, texture);
            }
        }
    }
}