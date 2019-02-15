using System;
using System.Collections.Generic;
using System.Text;
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine
{
    public class GenericTransition : Transition<GenericRuntimeState>
    {
        private Effect? possibleEffect;
     
        private string effectVariableName;
        
        private int? effectArgument;

        private Condition? possibleCondition;
       
        private string possibleCondVariableName;
       
        private int? condValue;

        public GenericTransition(string target, Effect? possibleEffect, string effectVariableName, int? effectArgument,
            Condition? cond, string possibleCondVariableName, int? condValue) : base(target)
        {
            this.possibleEffect = possibleEffect;
            this.effectVariableName = effectVariableName;
            this.effectArgument = effectArgument;
            this.possibleCondition = cond;
            this.possibleCondVariableName = possibleCondVariableName;
            this.condValue = condValue;
            if (possibleEffect != null && effectVariableName == null) throw new Exception("Inconsistent effect description");
        }

        public override bool IsApplicable(GenericRuntimeState extendedState)
        {
            if (possibleCondition == null) return true;
            if (possibleCondition == Condition.EQUAL)
            {
                return extendedState.Get(possibleCondVariableName) == condValue;
            }
            else if (possibleCondition == Condition.GREATER)
            {
                return extendedState.Get(possibleCondVariableName) > condValue;
            }
            else
            {
                throw new Exception("Illegal condition kind");
            }
        }

        public override void Effect(GenericRuntimeState extendedState)
        {
            if (possibleEffect == null) return;
            if(possibleEffect == FluentMachine.Effect.SET)
            {
                extendedState.Set(effectVariableName, effectArgument);
            } else if(possibleEffect == FluentMachine.Effect.CHANGE)
            {
                extendedState.Set(effectVariableName, extendedState.Get(effectVariableName) + effectArgument);
            } else
            {
                throw new Exception("Unknown effect");
            }
        }

        public new string ToString() {
            return "T(" + base.GetTarget() + "): " /**+ possibleEffect*/ + "@" + effectVariableName + "," /**+ effectArgument*/;
        }
    }
}
