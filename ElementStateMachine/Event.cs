using System;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
    /// <summary>
    /// A generic event class, where an incoming event is represented as a string
    /// This is a slightly modified version of Ulrik Pagh Schultz's Event class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    public class Event
    {
        /// <summary>
        /// The code of the event, as a string.
        /// </summary>
        private string code;

        /// <summary>
        /// Constructs a new Event with the given code
        /// </summary>
        /// <param name="code">the string representation of the event name</param>
        public Event(string code) => this.code = code;

        /// <summary>
        /// Get a string representation of the event code.
        /// </summary>
        /// <returns>the event code</returns>
        public string Code() => code;
    }
}
