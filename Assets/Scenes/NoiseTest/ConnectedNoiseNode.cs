using UnityEngine;

namespace Scenes.NoiseGeneratorTest {
    public abstract class ConnectedNoiseNode : NoiseNode {
        protected abstract void OnDrawGizmosSelected();

        protected void DrawConnection(NoiseNode incoming, Color color) {
            if (!incoming) {
                return;
            }
            Gizmos.color = color;
            Gizmos.DrawLine(incoming.transform.position, transform.position);
        }
    }
}