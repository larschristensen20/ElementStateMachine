﻿using System;
using System.Collections.Generic;

namespace ElementStateMachine
{
    public class MachineExecutor<T> where T : AbstractRuntimeState<T>
    {
        private string initialStateName;

        private Dictionary<string, State<T>> states = new Dictionary<string, State<T>>();

        private T runtime;

        public MachineExecutor(IMachineDescription<T> description)
        {
            List<State<T>> allStates = description.GetAllStates();
            foreach (State<T> state in allStates)
            {
                states.Add(state.GetName(), state);

            }
            initialStateName = allStates[0].GetName();
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
    }

    
}