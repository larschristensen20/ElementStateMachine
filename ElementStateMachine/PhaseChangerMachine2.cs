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
                Transition("SUPERHEAT").To("GAS").
                Transition("HEAT").To("GLASS").
            State("GLASS").
                Transition("COOL").To("GLASS").
                Transition("HEAT").To("LIQUID").
            State("LIQUID").
                Transition("COOL").To("SOLID").
                Transition("COOL").To("GLASS").
                Transition("SUPERCOOL").To("SUPERFLUID").
                Transition("HEAT").To("GAS").
            State("SUPERFLUID").
                Transition("SUPERHEAT").To("LIQUID").
            State("GAS").
                Transition("COOL").To("LIQUID").
                Transition("SUPERCOOL").To("SOLID").
                Transition("SUPERHEAT").To("PLASMA").
                Transition("SUPERCOOL").To("BOSE EINSTEIN CONDENSATE").
                Transition("HEAT").To("PLASMA").
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
