/*
Copyright (c) 2012, Ulrik Pagh Schultz, University of Southern Denmark
All rights reserved.

Redistribution and use in source and binary forms, with or without
modification, are permitted provided that the following conditions are met: 

1. Redistributions of source code must retain the above copyright notice, this
   list of conditions and the following disclaimer. 
2. Redistributions in binary form must reproduce the above copyright notice,
   this list of conditions and the following disclaimer in the documentation
   and/or other materials provided with the distribution. 

THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS "AS IS" AND
ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
(INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES;
LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND
ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
(INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.

The views and conclusions contained in the software and documentation are those
of the authors and should not be interpreted as representing official policies, 
either expressed or implied, of the University of Southern Denmark.
*/

using System.Collections.Generic;

namespace ElementStateMachine
{
    /// <summary>
    /// Metamodel for the state machine, constructed by FluentMachine and used at runtime by MachineExecutor
    /// This is a slightly modified version of Ulrik Pagh Schultz's MachineMetaModel class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    public class MachineMetaModel : IMachineDescription<GenericRuntimeState>
    {
        /// <summary>
        /// The complete list of all states (first is assumed to be initial)
        /// </summary>
        private List<State<GenericRuntimeState>> allStates;

        /// <summary>
        /// Set containing names of all extended state variables
        /// </summary>
        private HashSet<string> extendedStateVariables;

        /// <summary>
        /// Initialize metamodel
        /// </summary>
        /// <param name="states">a list of states</param>
        /// <param name="variables">a list of extendedstates</param>
        public MachineMetaModel(List<State<GenericRuntimeState>> states, HashSet<string> variables)
        {
            this.allStates = new List<State<GenericRuntimeState>>(states);
            this.extendedStateVariables = new HashSet<string>(variables);
        }

        /// <summary>
        /// Create a runtime state representation based on this metamodel
        /// </summary>
        /// <returns>a runtime state object representing the current state of the metamodel</returns>
        public GenericRuntimeState CreateRuntimeState()
        {
            return new GenericRuntimeState(extendedStateVariables);
        }
        /// <summary>
        /// Get all states in the metamodel
        /// </summary>
        /// <returns>the list of states</returns>
        public List<State<GenericRuntimeState>> GetAllStates()
        {
            return allStates;
        }
        /// <summary>
        /// Get the names of all extended state variables in the metamodel
        /// </summary>
        /// <returns>the list of names</returns>
        public HashSet<string> GetExtendedStateVariables()
        {
            return extendedStateVariables;
        }

    }
}


