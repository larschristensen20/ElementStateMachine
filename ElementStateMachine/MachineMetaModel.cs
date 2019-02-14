using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ElementStateMachine
{
    public class MachineMetaModel : IMachineDescription<GenericRuntimeState>
    {
        private List<State<GenericRuntimeState>> allStates;

        private HashSet<string> extendedStateVariables;

        public MachineMetaModel(List<State<GenericRuntimeState>> states, HashSet<string> variables)
        {
            this.allStates = new List<State<GenericRuntimeState>>(states);
            this.extendedStateVariables = new HashSet<string>(variables);
        }

        public GenericRuntimeState CreateRuntimeState()
        {
            return new GenericRuntimeState(extendedStateVariables);
        }

        public List<State<GenericRuntimeState>> GetAllStates()
        {
            return allStates;
        }
        
        public HashSet<string> GetExtendedStateVariables()
        {
            return extendedStateVariables;
        }

    }
}


