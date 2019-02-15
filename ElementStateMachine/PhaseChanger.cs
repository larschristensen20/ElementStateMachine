using System;
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine
{
    class PhaseChanger
    {
        static void Main(string[] args)
        {
            new MachineExecutor<GenericRuntimeState>(new PhaseChangerMachine3().GetMetaModel());

            new PhaseChangerMachine2();
        }
    }
}
