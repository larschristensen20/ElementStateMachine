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
                Transition("HEAT").To("LIQUID").SetState("LIQUID").
                Transition("SUPERHEAT").To("GAS").SetState("GAS").
            State("LIQUID").
                Transition("HEAT").To("GAS").SetState("GAS").
                Transition("COOL").To("SOLID").SetState("SOLID").
            State("GAS").
                Transition("HEAT").To("PLASMA").SetState("PLASMA").
                Transition("COOL").To("LIQUID").SetState("LIQUID").
                Transition("SUPERCOOL").To("SOLID").SetState("SOLID").
                Transition("SUPERHEAT").To("PLASMA").SetState("PLASMA").
            State("PLASMA").
                Transition("COOL").To("GAS").SetState("GAS");
        }
    }
}
