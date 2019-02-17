using System;
using System.Collections.Generic;
using System.Text;

namespace ElementStateMachine
{
    class NullDictionary<T,U> where U: class
    {
        private Dictionary<T, U> dict;

        public NullDictionary()
        {
            dict = new Dictionary<T, U>();
        }

        public U this[T key]
        {
            get
            {
                if (dict.ContainsKey(key))
                    return dict[key];
                else
                    return null;
            }
            set
            {
                dict[key] = value;
            }
        }

        public void Add(T key, U value)
        {
            dict.Add(key, value);
        }
    }
}
