using System;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
    public class PhaseChangerMachine3 : FluentMachine
    {
        public override void Build()
        {
            State("SOLID").
                Transition("SUPERHEAT").To("GAS").
                Transition("HEAT").To("LIQUID").
            State("LIQUID").
                Transition("COOL").To("SOLID").
                Transition("HEAT").To("GAS").
            State("GAS").
                Transition("COOL").To("LIQUID").
                Transition("SUPERCOOL").To("SOLID").
                Transition("SUPERHEAT").To("PLASMA").
                Transition("HEAT").To("PLASMA").
            State("PLASMA").
                Transition("COOL").To("GAS");
        }
    }
}

