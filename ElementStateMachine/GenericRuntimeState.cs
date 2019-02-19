using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

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
