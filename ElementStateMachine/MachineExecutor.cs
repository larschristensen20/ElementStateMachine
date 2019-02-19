using System;
using System.Collections.Generic;
using System.Linq;

namespace ElementStateMachine
{
    /// <summary>
    /// State machine executor: executes a state machine description,
    /// keeping track of current state, stores a map from state name
    /// to state object (used to perform state transitions)
    /// This is a slightly modified version of Ulrik Pagh Schultz's MachineExecutor interface found at https://github.com/ulrikpaghschultz/MDSD.git 
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