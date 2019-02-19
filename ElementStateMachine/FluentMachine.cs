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
        private MachineMetaModel metamodel;

        public MachineMetaModel GetMetaModel()
        {
            this.BuildMachine();
            return metamodel;
        }

        public enum Effect { SET, CHANGE }
        public enum Condition { EQUAL, GREATER }

        private List<State<GenericRuntimeState>> states = new List<State<GenericRuntimeState>>();

        private HashSet<string> variables = new HashSet<string>();

        private State<GenericRuntimeState> currentState;

        private string pendingEvent, targetTransition;
            
        private string effectVariable;

        private Effect? possibleEffect;

        private int? effectArgument;

        private TransitionFactory factory = new TransitionFactory();


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

        protected abstract void Build();

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

        public FluentMachine Transition(string e)
        {
            if (targetTransition != null) FlushTransition(null, null, 0);
            pendingEvent = e;
            return this;
        }

        public FluentMachine To(string state)
        {
            targetTransition = state;
            return this;
        }

        public FluentMachine SetState(string varName, int value)
        {
            possibleEffect = Effect.SET;
            if (!variables.Contains(varName)) throw new Exception("Undefined variable: " + varName);
            effectVariable = varName;
            effectArgument = value;
            return this;
        }

        public FluentMachine ChangeState(string varName, int value)
        {
            possibleEffect = Effect.CHANGE;
            if (!variables.Contains(varName)) throw new Exception("Undefined variable: " + varName);
            effectVariable = varName;
            effectArgument = value;
            return this;
        }

        public FluentMachine WhenStateEquals(string varName, int value)
        {
            FlushTransition(Condition.EQUAL, varName, value);
            return this;
        }

        public FluentMachine WhenStateGreaterThan(string varName, int value)
        {
            FlushTransition(Condition.GREATER, varName, value);
            return this;
        }

        public FluentMachine Otherwise()
        {
            FlushTransition(null, null, 0);
            return this;
        }

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

        public void SetTransitionFactory(TransitionFactory transitionFactory)
        {
            this.factory = transitionFactory;
        }

        public void IntegerState(string name)
        {
            this.metamodel.GetExtendedStateVariables().Add(name);
        }


        public class TransitionFactory
        {
            public Transition<GenericRuntimeState> CreateTransitionHook(string target, Effect? effect, string effectVarName, int? effectArg,
                Condition? condition, string possibleCondVariable, int? condValue)
            {
                return new GenericTransition(target, effect, effectVarName, effectArg, condition, possibleCondVariable, condValue);
            }
        }

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
