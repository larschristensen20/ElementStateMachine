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

namespace ElementStateMachine
{
    /// <summary>
    /// Abstract baseclass for statemachine runtime states that can be specialized to hold the extended state required for a given statemachine
    /// This is a slightly modified version of Ulrik Pagh Schultz's AbstractRuntimeState class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    /// <typeparam name="T">The specific type required to hold the extended state</typeparam>
    public abstract class AbstractRuntimeState<T> where T : AbstractRuntimeState<T>
    {
        /// <summary>
        /// The current state of the statemachine
        /// </summary>
        private State<T> currentState;

        /// <summary>
        /// Set the current state of the state machine
        /// </summary>
        /// <param name="state">the new state</param>
        public virtual void SetState(State<T> state) => currentState = state;

        /// <summary>
        /// Get the current state of the statemachine
        /// </summary>
        public virtual State<T> GetState => currentState;

        /// <summary>
        /// Reset the runtime state
        /// </summary>
        public virtual void ResetExtendedState() {; }

        /// <summary>
        /// Get the name of the currently executing state
        /// </summary>
        /// <returns>The name of the current state</returns>
        public virtual string GetStateName() => currentState.ToString();

        /// <summary>
        /// Get a string representation of the value of an extended state variable
        /// </summary>
        /// <param name="s">the name of the variable</param>
        /// <returns>string representation of the state</returns>
        public virtual string GetExtendedState(string s) { if (s == null) return currentState.GetName(); else throw new Exception("Bad name for extended state: " + s); }
    }
}
