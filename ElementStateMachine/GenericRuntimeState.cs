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
    /// Generic extended state representation based on a map
    /// This is a slightly modified version of Ulrik Pagh Schultz's GenericRuntimeState class found at https://github.com/ulrikpaghschultz/MDSD.git 
    /// </summary>
    public class GenericRuntimeState : AbstractRuntimeState<GenericRuntimeState>
    {
        private Dictionary<string, int?> values = new Dictionary<string, int?>();

        public GenericRuntimeState(HashSet<string> variables)
        {
            foreach (string v in variables)
                values.Add(v, 0);
        }

        public int? Get(string name)
        {
            if (!values.ContainsKey(name)) throw new Exception("Undeclared variable: " + name);
            return values[name];
        }

        public void Set(string name, int? value)
        {
            if (!values.ContainsKey(name)) throw new Exception("Undeclared variable: " + name);
            values.Add(name, value);
        }

        public override void ResetExtendedState()
        {
            foreach (KeyValuePair<string, int?> vars in values)
            {
                values[vars.Key] = 0;
            }
        }

        public override string GetExtendedState(string s)
        {
            if (s == null) return base.GetExtendedState(s);
            else return this.Get(s).ToString();                
        }


    }
}
