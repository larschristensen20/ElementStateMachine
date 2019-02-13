using System;
using System.Collections.Generic;
using System.Text;

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
        public void SetState(State<T> state) => currentState = state;

        /// <summary>
        /// Get the current state of the statemachine
        /// </summary>
        public State<T> GetState => currentState;

        /// <summary>
        /// Reset the runtime state
        /// </summary>
        public void ResetExtendedState() {; }

        /// <summary>
        /// Get the name of the currently executing state
        /// </summary>
        /// <returns>The name of the current state</returns>
        public string GetStateName() => currentState.ToString();

        /// <summary>
        /// Get a string representation of the value of an extended state variable
        /// </summary>
        /// <param name="s">the name of the variable</param>
        /// <returns>string representation of the state</returns>
        public string GetExtendedState(string s) { if (s == null) return currentState.GetName(); else throw new Exception("Bad name for extended state: " + s); }
    }
}
