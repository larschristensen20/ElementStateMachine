using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
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

        public abstract void Build();

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
            if (targetTransition != null || possibleEffect != null) FlushTransition(null, null, 0);
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
