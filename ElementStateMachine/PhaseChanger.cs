using System;
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine
{
    class Program
    {
        static void Main(string[] args)
        {
            new PhaseChangerMachine1();
            
            new PhaseChangerMachine2();
        }
    }
}
