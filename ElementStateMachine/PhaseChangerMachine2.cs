using System;
using System.Collections.Generic;
using System.Text;
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine 
{
    class PhaseChangerMachine2 : FluentMachine
    {
        public override void Build()
        {
            State("SOLID").
                Transition("HEAT").To("LIQUID").
                Transition("HEAT").To("GLASS").
                Transition("SUPERHEAT").To("GAS").
            State("GLASS").
                Transition("HEAT").To("LIQUID").
                Transition("COOL").To("GLASS").
            State("LIQUID").
                Transition("HEAT").To("GAS").
                Transition("COOL").To("SOLID").
                Transition("COOL").To("GLASS").
                Transition("SUPERCOOL").To("SUPERFLUID").
            State("SUPERFLUID").
                Transition("SUPERHEAT").To("LIQUID").
            State("GAS").
                Transition("HEAT").To("PLASMA").
                Transition("COOL").To("LIQUID").
                Transition("SUPERCOOL").To("SOLID").
                Transition("SUPERHEAT").To("PLASMA").
                Transition("SUPERCOOL").To("BOSE EINSTEIN CONDENSATE").
            State("BOSE EINSTEIN CONDENSATE").
                Transition("SUPERHEAT").To("GAS").
            State("PLASMA").
                Transition("COOL").To("GAS").
                Transition("SUPERHEAT").To("QUARK-GLUON PLASMA").
            State("QUARK-GLUON PLASMA").
                Transition("SUPERCOOL").To("PLASMA");
                
        }
    }
}
