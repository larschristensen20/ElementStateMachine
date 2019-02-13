﻿using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Collections.ObjectModel;

namespace ElementStateMachine
{
    public class State<T> where T: AbstractRuntimeState<T>
    {
        private string name;
        private Dictionary<string, List<Transition<T>>> Transitions = new Dictionary<string, List<Transition<T>>>();

        public State(string name)
        {
            this.name = name;
        }

        public void AddTransition(string eventName, Transition<T> transition)
        {
            List<Transition<T>> matches = Transitions[eventName];
            if (matches == null)
            {
                matches = new List<Transition<T>>();
                Transitions.Add(eventName, matches);
            }
            matches.Add(transition);
        }

        public void ProcessEvent(MachineExecutor<T> machine, T runtime, Event e) {
            List<Transition<T>> matches = Transitions[e.Code()];
            if (matches == null) return;
            foreach (Transition<T> transition in matches)
            {
                if(transition.IsApplicable(runtime))
                {
                    string newMaybe = transition.Action(runtime);
                    if (newMaybe != null) machine.SetState(newMaybe);
                    return;
                }
            }
        }

        public new string ToString => GetName();

        public string GetName() => name;

        public HashSet<string> ApplicableEvents => new HashSet<string>(Transitions.Keys);

        public List<Transition<T>> GetTransitionsForEvent(string e) => Transitions[e];

        public Dictionary<string, List<Transition<T>>> GetAllTransitions() => Transitions;
    }
}