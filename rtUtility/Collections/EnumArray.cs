// System
using System;
using System.Collections;
using System.Collections.Generic;

namespace rtUtility.Collections
{
    public interface IReadOnlyEnumArray<TKey, TValue> : IEnumerable<KeyValuePair<TKey, TValue>>
    {
        TValue this[TKey aKey] { get; }
    }

    public interface IEnumArray<TKey, TValue> : IReadOnlyEnumArray<TKey, TValue>
    {
        new TValue this[TKey aKey] { get; set; }
    }

    public class TEnumArray<TKey, TValue> : IEnumArray<TKey, TValue> where TKey : struct
    {
        public TEnumArray()
        {
            if (!(typeof(TKey).IsEnum))
                throw new Exception("Only enum allowed for key.");

            foreach (TKey e in Enum.GetValues(typeof(TKey))) {
                p_Dictionary.Add(e, default(TValue));
            }

            return;
        }

        public TValue this[TKey aKey]
        {
            get { return p_Dictionary[aKey]; }
            set { p_Dictionary[aKey] = value; }
        }

        private Dictionary<TKey, TValue> p_Dictionary = new Dictionary<TKey, TValue>();

        IEnumerator<KeyValuePair<TKey, TValue>> IEnumerable<KeyValuePair<TKey, TValue>>.GetEnumerator()
        {
            return p_Dictionary.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return p_Dictionary.GetEnumerator();
        }
    }
}
