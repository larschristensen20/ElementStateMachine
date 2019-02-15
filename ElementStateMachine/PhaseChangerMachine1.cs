using System;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
    public class PhaseChangerMachine1 : FluentMachine 
    {
        public override void Build()
        {
            State("SOLID").
                Transition("HEAT").To("LIQUID").
                Transition("SUPERHEAT").To("GAS").
            State("LIQUID").
                Transition("HEAT").To("GAS").
                Transition("COOL").To("SOLID").
            State("GAS").
                Transition("HEAT").To("PLASMA").
                Transition("COOL").To("LIQUID").
                Transition("SUPERCOOL").To("SOLID").
                Transition("SUPERHEAT").To("PLASMA").
            State("PLASMA").
                Transition("COOL").To("GAS");
        }
    }
}
