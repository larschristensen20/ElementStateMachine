using System;

namespace ElementStateMachine
{
    public class Transition<T>
    {
        private string targetState;

        public Transition(string targetState) { this.targetState = targetState; }

        public virtual string Action(T extendedState)
        {
            Effect(extendedState);
            return targetState;
        }

        public virtual string GetTarget() => targetState;

        public virtual void Effect(T extendedState) { ; }

        public virtual bool IsApplicable(T extendedState) => true;
       
    }
}