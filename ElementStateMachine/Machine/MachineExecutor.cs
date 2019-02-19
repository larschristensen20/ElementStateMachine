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
using System;
using System.Collections.Generic;

namespace ElementStateMachine
{
    /// <summary>
    /// State machine executor: executes a state machine description,
    /// keeping track of current state, stores a map from state name
    /// to state object (used to perform state transitions)
    /// This is a slightly modified version of Ulrik Pagh Schultz's MachineExecutor class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class MachineExecutor<T> where T : AbstractRuntimeState<T>
    {
        private string initialStateName;

        private Dictionary<string, State<T>> states = new Dictionary<string, State<T>>();

        private T runtime;

        public MachineExecutor(IMachineDescription<T> description)
        {
            List<State<T>> allStates = description.GetAllStates();
            Console.WriteLine("\n"+"State Machine created..." + "Possible states: ");
            foreach (State<T> state in allStates)
            { 
                states.Add(state.GetName(), state);
                states.TryGetValue(state.GetName(), out State<T> val);
                Console.WriteLine("   " + val.GetName());              
            }
            
            initialStateName = allStates[0].GetName();
            Console.WriteLine("Initial state: " + initialStateName);
            runtime = description.CreateRuntimeState();
        }

        public void Initialize()
        {
            runtime.ResetExtendedState();
            SetState(initialStateName);
        }

        public void SetState(string stateID)
        {
            State<T> state = states[stateID];
            if (state == null) throw new Exception("Illegal state identifier: " + stateID);
            runtime.SetState(state);
        }

        public string GetStateName() => runtime.GetStateName();

        public List<Transition<T>> GetTransitionForEvent(string e) => runtime.GetState.GetTransitionsForEvent(e);

        public void ProcessEvent(Event e)
        {
            if (runtime.GetState == null) throw new Exception("State machine not initialized");
            runtime.GetState.ProcessEvent(this, runtime, e);
        }

        public string GetRuntimeState(string s)
        {
            if (s == null)
                return this.GetStateName();
            else
                return runtime.GetExtendedState(s);
        }

        public void Format()
        {
            //foreach (State<T> s in description.GetAllStates())
            //{
            //    s.ToString();
            //    s.GetAllTransitions().ToString();
                
            //}
        }

    }

    
}