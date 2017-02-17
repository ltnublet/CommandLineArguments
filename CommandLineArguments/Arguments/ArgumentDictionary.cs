using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arguments
{
    internal class ArgumentDictionary : IDictionary<string, AttributeField[]>
    {
        public AttributeField[] this[string key]
        {
            get
            {

            }
            set
            {

            }
        }

        public ICollection<string> Keys
        {
            get
            {

            }
        }

        public ICollection<AttributeField[]> Values => throw new NotImplementedException();

        public int Count => throw new NotImplementedException();

        public bool IsReadOnly { get; private set; }

        private Dictionary<string, AttributeField[]> innerDictionary;

        public void Add(string key, AttributeField[] value)
        {
            throw new NotImplementedException();
        }

        public void Add(KeyValuePair<string, AttributeField[]> item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(KeyValuePair<string, AttributeField[]> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(string key)
        {
            throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<string, AttributeField[]>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<string, AttributeField[]>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(string key)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<string, AttributeField[]> item)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(string key, out AttributeField[] value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
