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
    /// A generic state representation: belongs to a machine, has a name, and a number
    /// of transitions. When an event is delivered, the transitions are tested in the
    /// same order as they were initially inserted into the state, the first one that is
    /// applicable is performed by executing its effect (if any) and performing the
    /// corresponding transition (if any).
    /// This is a slightly modified version of Ulrik Pagh Schultz's State class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class State<T> where T : AbstractRuntimeState<T>
    {
        /// <summary>
        /// The name of the state
        /// </summary>
        private string name;

        /// <summary>
        /// Transitions associated with this state
        /// </summary>
        private Dictionary<string, List<Transition<T>>> transitions = new Dictionary<string, List<Transition<T>>>();

        /// <summary>
        /// Instantiate state for a given state machine
        /// </summary>
        /// <param name="name">the name of the state</param>
        public State(string name)
        {
            this.name = name;
        }

        /// <summary>
        /// Add a transition that is tested in this state when the corresponding event is received
        /// </summary>
        /// <param name="eventName">eventName name of event that can trigger this transition</param>
        /// <param name="transition">transition the transition</param>
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

        /// <summary>
        /// Process event by testing isApplicable in sequence, and then performing the action which
        /// may have an effect and returns the name of a new state, if a transition is to be made.
        /// </summary>
        /// <param name="machine">the machine, which state is set</param>
        /// <param name="runtime">the runtime</param>
        /// <param name="e">the event to process</param>
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
        /// <summary>
        /// Get the name of the state
        /// </summary>
        /// <returns></returns>
        public override string ToString()  => GetName();

        /// <summary>
        /// Get the name of the state
        /// </summary>
        /// <returns>the name</returns>
        public string GetName() => name;

        /// <summary>
        /// Get the set of events that can be handled in this state
        /// </summary>
        public HashSet<string> ApplicableEvents => new HashSet<string>(transitions.Keys);

        /// <summary>
        /// Get the set of transitions that correspond to a given event ID
        /// </summary>
        /// <param name="e">the event</param>
        /// <returns></returns>
        public List<Transition<T>> GetTransitionsForEvent(string e) => transitions[e];

        /// <summary>
        /// Get all transitions (introspection)
        /// </summary>
        /// <returns>a map of event names (as string) and a List<Transition<T>></returns>
        public Dictionary<string, List<Transition<T>>> GetAllTransitions() => transitions;
    }
}
