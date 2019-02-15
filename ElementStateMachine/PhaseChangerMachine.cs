using System;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
    public class PhaseChangerMachine : FluentMachine 
    {
        private static readonly int MAX_PHASE = 4;
        private static readonly int MIN_PHASE = 1;

        public override void Build()
        {
            IntegerState("started");
            State("LIQUID").Transition("HEAT").To("GAS").SetState("GAS", 3).State("GAS").
                Transition("HEAT").To("PLASMA").
                WhenStateEquals("started",MAX_PHASE).ChangeState("started",1).
                Otherwise().Transition("COOL").To("SOLID").WhenStateEquals("started",-1).Otherwise

        }
    }
}
