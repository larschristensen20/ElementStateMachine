using System;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
    /// <summary>
    /// State machine description: static description of the structure
    /// of a given statemachine(will eventually be evolved to play the metamodel role) 
    /// This is a slightly modified version of Ulrik Pagh Schultz's MachineDescription interface found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMachineDescription<T> where T: AbstractRuntimeState<T>
    {
        List<State<T>> GetAllStates();

        T CreateRuntimeState();
    }
}
