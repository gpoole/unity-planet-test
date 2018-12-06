using System.Linq;
using LibNoise;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest.Operator {
    public abstract class CombinerNode : ConfigurableNode {
        public override bool AutoLayout => true;

        [ValueConnectionKnob("Left", Direction.In, typeof(ModuleBase))]
        public ValueConnectionKnob lhsInputKnob;

        [ValueConnectionKnob("Right", Direction.In, typeof(ModuleBase))]
        public ValueConnectionKnob rhsInputKnob;

        [ValueConnectionKnob("Output", Direction.Out, typeof(ModuleBase))]
        public ValueConnectionKnob outputKnob;

        protected virtual int MinimumInputs => 2;

        public override bool Calculate() {
            var inputModules = inputKnobs.Cast<ValueConnectionKnob>().Where(knob => !knob.IsValueNull)
                .Select(knob => knob.GetValue<ModuleBase>()).ToArray();
            if (inputModules.Length < MinimumInputs) {
                outputKnob.ResetValue();
                return true;
            }
            outputKnob.SetValue(CreateModule(inputModules));
            return true;
        }

        protected abstract ModuleBase CreateModule(ModuleBase[] inputs);
    }
}