using System;
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine
{
    class PhaseChanger
    {
        static void Main(string[] args)
        {
            new MachineExecutor(new PhaseChangerMachine1().GetMetaModel());

            new PhaseChangerMachine2();
        }
    }
}
