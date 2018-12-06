using LibNoise;
using NodeEditorFramework;
using UnityEngine;

namespace Scenes.NoiseNodeTest.Operator {
    public abstract class TransformerNode : ConfigurableNode {
        public override bool AutoLayout => true;

		[ValueConnectionKnob("Input", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob inputKnob;

		[ValueConnectionKnob("Output", Direction.Out, typeof(ModuleBase))]
		public ValueConnectionKnob outputKnob;

	    public override bool Calculate() {
		    var input = inputKnob.GetValue<ModuleBase>();
		    if (input == null) {
			    outputKnob.ResetValue();
			    return true;
		    }
		    outputKnob.SetValue(CreateModule(input));
		    return true;
	    }

	    protected abstract ModuleBase CreateModule(ModuleBase input);
    }
}