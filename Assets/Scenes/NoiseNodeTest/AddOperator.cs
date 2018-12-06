using System.Reflection;
using LibNoise;
using LibNoise.Operator;
using NodeEditorFramework;

namespace Scenes.NoiseNodeTest {
	[Node (false, "Noise/Operations/Add")]
    public class AddOperator : Node {
        private const string ID = "NoiseAddOperator";

        public override string GetID {
            get { return ID; }
        }

        public override string Title {
            get { return "Add Noise"; }
        }

		[ValueConnectionKnob("LHS", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob lhsInputKnob;

		[ValueConnectionKnob("RHS", Direction.In, typeof(ModuleBase))]
		public ValueConnectionKnob rhsInputKnob;

		[ValueConnectionKnob("Output", Direction.Out, typeof(ModuleBase))]
		public ValueConnectionKnob outputKnob;

	    private ModuleBase _lhs;

	    private ModuleBase _rhs;
		
		public override void NodeGUI () 
		{
			lhsInputKnob.DisplayLayout();
			rhsInputKnob.DisplayLayout();
			outputKnob.DisplayLayout();
		}

	    public override bool Calculate() {
		    _lhs = lhsInputKnob.GetValue<ModuleBase>();
		    _rhs = rhsInputKnob.GetValue<ModuleBase>();
		    if (_lhs == null || _rhs == null) {
			    outputKnob.ResetValue();
			    return true;
		    }
		    outputKnob.SetValue(new Add(_lhs, _rhs));
		    return true;
	    }
    }
}