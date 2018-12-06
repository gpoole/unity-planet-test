using NodeEditorFramework;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
    public abstract class ConfigurableNode : Node {

        public override void NodeGUI() {
	        base.NodeGUI();

			DrawConfigurationGUI();

            if (GUI.changed) {
                NodeEditor.curNodeCanvas.OnNodeChange(this);
            }
        }

	    protected virtual void DrawConfigurationGUI() { }
    }
}