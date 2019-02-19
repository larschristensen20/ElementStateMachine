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
using static ElementStateMachine.FluentMachine;

namespace ElementStateMachine
{
    /// <summary>
    /// Generic transition that performs its function depending on its description
    /// of effects and conditions(passed as parameters, at most one of each)
    /// This is a slightly modified version of Ulrik Pagh Schultz's GenericTransition class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
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

        public override string ToString() {
            return "T(" + base.GetTarget() + "): " /**+ possibleEffect*/ + "@" + effectVariableName + "," /**+ effectArgument*/;
        }
    }
}
