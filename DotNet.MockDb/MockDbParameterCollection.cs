using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace DotNet.MockDb
{
    public class MockDbParameterCollection: List<IDataParameter>, IDataParameterCollection
    {
        public bool Contains(string parameterName)
        {
            return IndexOf(parameterName) != -1;
        }

        public int IndexOf(string parameterName)
        {
            var parameter = this.FirstOrDefault(x => x.ParameterName == parameterName);
            return parameter == null? -1 : IndexOf(parameter);
        }

        public void RemoveAt(string parameterName)
        {
            var index = IndexOf(parameterName);
            if (index != -1)
                RemoveAt(index);
        }

        public object this[string parameterName]
        {
            get { return this.FirstOrDefault(x => x.ParameterName == parameterName); }
            set
            {
                var index = IndexOf(parameterName);
                if (index == -1)
                {
                    Add(value as IDataParameter);
                }
                else
                {
                    this[index] = value as IDataParameter;
                }
            }
        }
    }
}