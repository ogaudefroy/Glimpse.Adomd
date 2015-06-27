using Microsoft.AnalysisServices.AdomdClient;

namespace Glimpse.Adomd.Sample.Stubs
{
    using System;
    using System.Collections;
    using System.Data;

    public class FakeDataParameterCollection : IDataParameterCollection
    {
        private readonly ArrayList _items = new ArrayList();

        public bool Contains(string parameterName)
        {
            return -1 != this.IndexOf(parameterName);
        }

        public int IndexOf(string parameterName)
        {
            int count = _items.Count;
            for (int i = 0; i < count; i++)
            {
                if (parameterName == ((IDataParameter)_items[i]).ParameterName)
                {
                    return i;
                }
            }
            return -1;
        }

        public void RemoveAt(string parameterName)
        {
            _items.RemoveAt(this.IndexOf(parameterName));
        }

        public object this[string parameterName]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value)
        {
            throw new NotImplementedException();
        }

        public int IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        public bool IsFixedSize
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public object this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsSynchronized
        {
            get { throw new NotImplementedException(); }
        }

        public object SyncRoot
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}