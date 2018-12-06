using System.Collections;
using UnityEngine;

namespace Scenes.NoiseNodeTest {
    public class CoroutineSpawner : MonoBehaviour {

        private static CoroutineSpawner Instance {
            get {
                if (!_instance) {
                    _instance = (new GameObject("[CoroutineSpawner]")).AddComponent<CoroutineSpawner>();
                }
                return _instance;
            }
        }

        private static CoroutineSpawner _instance;

        public static Coroutine Start(IEnumerator routine) {
            return Instance.StartCoroutine(routine);
        }

        public static void Stop(Coroutine coroutine) {
            Instance.StopCoroutine(coroutine);
        }
    }
}