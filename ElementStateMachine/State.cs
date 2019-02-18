using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Collections.ObjectModel;

namespace ElementStateMachine
{
    public class State<T> where T : AbstractRuntimeState<T>
    {
        private string name;
        private Dictionary<string, List<Transition<T>>> transitions = new Dictionary<string, List<Transition<T>>>();

        public State(string name)
        {
            this.name = name;
        }

        public void AddTransition(string eventName, Transition<T> transition)
        { 
            List<Transition<T>> matches = transitions.GetValueOrDefault(eventName);
                if (matches == null)
                {
                    matches = new List<Transition<T>>();
                    transitions.Add(eventName, matches);
                }
                matches.Add(transition);
        }

        public void ProcessEvent(MachineExecutor<T> machine, T runtime, Event e) {
               List<Transition<T>> matches = transitions.GetValueOrDefault(e.Code());
            if (matches == null) return;
            foreach (Transition<T> transition in matches)
            {
                if(transition.IsApplicable(runtime))
                {
                    string possibleTransition = transition.Action(runtime);
                    if (possibleTransition != null) machine.SetState(possibleTransition);
                    return;
                }
            }
        }

        public override string ToString()  => GetName();

        public string GetName() => name;

        public HashSet<string> ApplicableEvents => new HashSet<string>(transitions.Keys);

        public List<Transition<T>> GetTransitionsForEvent(string e) => transitions[e];

        public Dictionary<string, List<Transition<T>>> GetAllTransitions() => transitions;
    }
}
