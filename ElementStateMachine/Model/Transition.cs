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
namespace ElementStateMachine
{
    /// <summary>
    /// Generic template class for transitions.  Simple transitions can use this class directly,
    /// conditions can be added by overriding the isApplicable method, effects can be added by
    /// overriding the effect method.
    /// This is a slightly modified version of Ulrik Pagh Schultz's Transition class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    public class Transition<T>
    {

        /// <summary>
        /// The target state of the transition
        /// </summary>
        private string targetState;

        /// <summary>
        /// Create a transition that targets the given state
        /// </summary>
        /// <param name="targetState">the target state</param>
        public Transition(string targetState) { this.targetState = targetState; }

        /// <summary>
        /// The action of the state: perform effect (as defined by hook method), returning the target state
        /// </summary>
        /// <param name="extendedState">the extendedState which has an effect</param>
        /// <returns>the target state</returns>
        public virtual string Action(T extendedState)
        {
            Effect(extendedState);
            return targetState;
        }

        /// <summary>
        /// Provide name of target state that should be transitioned to
        /// </summary>
        /// <returns>name of the target state</returns>
        public virtual string GetTarget() => targetState;

        /// <summary>
        /// Hook method: override to provide effect 
        /// </summary>
        /// <param name="extendedState">the extendedState that has an effect</param>
        public virtual void Effect(T extendedState) { ; }

        /// <summary>
        /// Hook method: override to provide condition
        /// </summary>
        /// <param name="extendedState">the extendedState to check</param>
        /// <returns>false if this transition should not take place now, true otherwise</returns>
        public virtual bool IsApplicable(T extendedState) => true;

    }
}