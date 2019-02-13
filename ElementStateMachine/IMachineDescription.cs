using System;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
    public interface IMachineDescription<T> where T: AbstractRuntimeState<T>
    {
        List<State<T>> GetAllStates();

        T CreateRuntimeState();
    }
}
