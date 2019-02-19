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
    /// Abstract class providing a fluent interface for defining a state machine using the generic statemachine framework(year2).
    /// Acts as a builder but also allows the model to be directly interpreted to run the state machine.
    /// This is a slightly modified version of Ulrik Pagh Schultz's FluentMachine class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    public abstract class FluentMachine
    {
        /// <summary>
        /// The statemachine metamodel
        /// </summary>
        private MachineMetaModel metamodel;
  
        public MachineMetaModel GetMetaModel()
        {
            this.BuildMachine();
            return metamodel;
        }

        /// <summary>
        /// Builder infrastructure
        /// </summary>

        // Enums defining types of effects and conditions that can be used

        /// <summary>
        /// Effect of a state: set an extended state variable, or change to a different state
        /// </summary>
        public enum Effect { SET, CHANGE }

        /// <summary>
        /// Condition on a state: variable equal to the given value or greater than the given value
        /// </summary>
        public enum Condition { EQUAL, GREATER }

        // Accumulating variables for the builder

        /// <summary>
        /// All states that have been defined
        /// </summary>
        private List<State<GenericRuntimeState>> states = new List<State<GenericRuntimeState>>();

        /// <summary>
        /// All extended state variables that have been defined
        /// </summary>
        private HashSet<string> variables = new HashSet<string>();

        /// <summary>
        /// The current state being built
        /// </summary>
        private State<GenericRuntimeState> currentState;

        /// <summary>
        /// The current event that transitions are being defined for
        /// </summary>
        private string pendingEvent;

        /// <summary>
        /// The target of the current transition
        /// </summary>
        private string targetTransition;

        /// <summary>
        /// The name of the mutable state acted upon by the effect on the current transition, if any
        /// </summary>
        private string effectVariable;

        /// <summary>
        /// The effect of the current transition, if any
        /// </summary>
        private Effect? possibleEffect;

        /// <summary>
        /// The constant argument to the effect on the current transition, if any
        /// </summary>
        private int? effectArgument;

        /// <summary>
        /// A factory object for creating transition instances, overwrite default to control transition creation
        /// </summary>
        private TransitionFactory factory = new TransitionFactory();

        /// <summary>
        /// Build a machine using the fluent interface.  First call build (overridden in subclass),
        /// then ensure that all transitions associated with the current state have been defined,
        /// and last add the state as the last in the list of states
        /// </summary>
        public void BuildMachine()
        {
            if (metamodel != null) return;
            Build();
            FlushTransition(null, null, 0);
            if (currentState == null) throw new Exception("Empty Statemachine definition");
            states.Add(currentState);
            CheckNameConsistency();
            metamodel = new MachineMetaModel(states, variables);
        }

        /// <summary>
        /// Override in subclasses, must define state machine using fluent interface (and initialize state variables)
        /// </summary>
        protected abstract void Build();

        /// <summary>
        /// Start a new state, of the given name
        /// </summary>
        /// <param name="name">name of the state</param>
        /// <returns>the FluentMachine builder</returns>
        public FluentMachine State(string name)
        {
            if(currentState != null)
            {
                FlushTransition(null, null, 0);
                states.Add(currentState);
            }
            currentState = new State<GenericRuntimeState>(name);
            return this;
        }

        /// <summary>
        /// Define a new transition, in the context of the current state
        /// </summary>
        /// <param e="e">name of the Event</param>
        /// <returns>the FluentMachine builder</returns>
        public FluentMachine Transition(string e)
        {
            if (targetTransition != null) FlushTransition(null, null, 0);
            pendingEvent = e;
            return this;
        }

        /// <summary>
        ///  Name the target state of the current transition
        /// </summary>
        /// <param state="state">name of the state</param>
        /// <returns>the FluentMachine builder</returns>
        public FluentMachine To(string state)
        {
            targetTransition = state;
            return this;
        }

        /// <summary>
        /// Include a set state effect in the current transition
        /// </summary>
        /// <param name="varName">the variable to set</param>
        /// <param name="value">the value to set it to</param>
        /// <returns>the FluentMachine builder</returns>
        public FluentMachine SetState(string varName, int value)
        {
            possibleEffect = Effect.SET;
            if (!variables.Contains(varName)) throw new Exception("Undefined variable: " + varName);
            effectVariable = varName;
            effectArgument = value;
            return this;
        }

        /// <summary>
        /// Include a change state effect in the current transition
        /// </summary>
        /// <param name="varName">the variable to change</param>
        /// <param name="value">the value to add to the variable</param>
        /// <returns>the FluentMachine builder</returns>
        public FluentMachine ChangeState(string varName, int value)
        {
            possibleEffect = Effect.CHANGE;
            if (!variables.Contains(varName)) throw new Exception("Undefined variable: " + varName);
            effectVariable = varName;
            effectArgument = value;
            return this;
        }

        /// <summary>
        /// Add an equals condition to the current transition
        /// </summary>
        /// <param name="varName">the variable to have a condition on</param>
        /// <param name="value">the value to compare to</param>
        /// <returns>the FluentMachine builder</returns>
        public FluentMachine WhenStateEquals(string varName, int value)
        {
            FlushTransition(Condition.EQUAL, varName, value);
            return this;
        }

        /// <summary>
        /// Add a comparison condition to the current transition
        /// </summary>
        /// <param name="varName">the variable to have a condition on</param>
        /// <param name="value">the value to compare to</param>
        /// <returns>the FluentMachine builder</returns>
        public FluentMachine WhenStateGreaterThan(string varName, int value)
        {
            FlushTransition(Condition.GREATER, varName, value);
            return this;
        }

        /// <summary>
        /// Indicate that a state is a simple alternative (syntactic sugar)
        /// </summary>
        /// <returns>the FluentMachine builder</returns>
        public FluentMachine Otherwise()
        {
            FlushTransition(null, null, 0);
            return this;
        }

        /// <summary>
        /// Flush the current transition, in preparation for the start of a new transition or state
        /// </summary>
        /// <param name="condition">the condition type, if any, on the transition being finalized</param>
        /// <param name="possibleCondVariableName">the variable to test on, if any</param>
        /// <param name="condValue">the value to compare to, if any</param>
        private void FlushTransition(Condition? condition, string possibleCondVariableName, int condValue)
        {
            if (pendingEvent == null) return;
            if (targetTransition == null && possibleEffect == null) return;

            Transition<GenericRuntimeState> transition = factory.CreateTransitionHook(targetTransition, possibleEffect, effectVariable, effectArgument,
                condition, possibleCondVariableName, condValue);
            currentState.AddTransition(pendingEvent, transition);

            possibleEffect = null;
            effectVariable = null;
            effectArgument = null;
        }

        /// <summary>
        /// Change the factory used by the builder to create transition objects
        /// </summary>
        /// <param name="transitionFactory">the new transition factory</param>
        public void SetTransitionFactory(TransitionFactory transitionFactory)
        {
            this.factory = transitionFactory;
        }

        /// <summary>
        /// Add a new integer state to the state machine
        /// </summary>
        /// <param name="name">the name of the extended state variable</param>
        public void IntegerState(string name)
        {
            this.metamodel.GetExtendedStateVariables().Add(name);
        }

        /// <summary>
        /// Abstract transition creation for allowing override in DSL variations
        /// </summary>
        public class TransitionFactory
        {
            /// <summary>
            /// Hook method allowing the creation of a transition to be changed, as specified by the arguments.
            /// </summary>
            /// <param name="target">the target of the transition</param>
            /// <param name="effect">the effect of the transition, if any</param>
            /// <param name="effectVarName">the variable that the transition has an effect on, if any</param>
            /// <param name="effectArg">the argument of the effect, if any</param>
            /// <param name="condition">the condition of the transition, if any</param>
            /// <param name="possibleCondVariable">the variable used in the condition, if any</param>
            /// <param name="condValue">the value used in the condition, if any</param>
            /// <returns>a transition object created according to the specification.</returns>
            internal Transition<GenericRuntimeState> CreateTransitionHook(string target, Effect? effect, string effectVarName, int? effectArg,
                Condition? condition, string possibleCondVariable, int? condValue)
            {
                return new GenericTransition(target, effect, effectVarName, effectArg, condition, possibleCondVariable, condValue);
            }
        }

        /// <summary>
        /// Check naming consistency, for now just state names
        /// </summary>
        private void CheckNameConsistency()
        {
            HashSet<string> allStateNames = new HashSet<string>();
            foreach (State<GenericRuntimeState> state in states)
            {
                allStateNames.Add(state.GetName());
            }
            foreach(State<GenericRuntimeState> state in states) {
                foreach (KeyValuePair<string, List<Transition<GenericRuntimeState>>> transitionBlob in state.GetAllTransitions()) {
                    foreach (Transition<GenericRuntimeState> transition in transitionBlob.Value) {
                        string target = transition.GetTarget();
                        if (target == null) continue;
                        if (!allStateNames.Contains(target)) throw new Exception("Illegal state name: " + transition.GetTarget());
                    }
                }
            }
        }
    }
}
