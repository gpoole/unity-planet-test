using LibNoise;
using UnityEngine;

namespace Scenes.NoiseGeneratorTest {
    public abstract class NoiseNode : MonoBehaviour {
        public abstract ModuleBase GetResult();
    }
}