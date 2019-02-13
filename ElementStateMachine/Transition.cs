using System;

namespace ElementStateMachine
{
    public class Transition<T>
    {
        private string targetState;

        public Transition(string targetState) { this.targetState = targetState; }

        public string Action(T extendedState)
        {
            Effect(extendedState);
            return targetState;
        }

        public string GetTarget() => targetState;

        protected void Effect(T extendedState) {; }

        public bool IsApplicable(T extendedState) => true;
       
    }
}