using System.Collections;
using LibNoise;
using UnityEngine;

namespace Scenes.NoiseGeneratorTest {
    public class NoiseRenderer : MonoBehaviour {
        private static readonly int PreviewSize = 128;
        
        private NoiseNode _node;

        private void Start() {
            _node = GetComponentInParent<NoiseNode>();
            StartCoroutine(UpdateTexture());
        }

        private IEnumerator UpdateTexture() {
            var wait = new WaitForSeconds(1.5f);
            while (true) {
                GenerateTexture();
                yield return wait;
            }
        }

        private void GenerateTexture() {
            if (!_node) {
                return;
            }
            var noise = _node.GetResult();
            if (noise == null) {
                return;
            }
            var noiseRenderer = new Noise2D(PreviewSize, noise);
            noiseRenderer.GeneratePlanar(-1, 1, -1, 1);
            var texture = noiseRenderer.GetTexture();

            var renderer = GetComponentInChildren<Renderer>();
            renderer.material.SetTexture("_MainTex", texture);
        }
    }
}