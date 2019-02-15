using System;
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine
{
    class PhaseChanger
    {
        static void Main(string[] args)
        {
            new MachineExecutor<GenericRuntimeState>(new PhaseChangerMachine2().GetMetaModel());
            new MachineExecutor<GenericRuntimeState>(new PhaseChangerMachine1().GetMetaModel());
        }
    }
}
