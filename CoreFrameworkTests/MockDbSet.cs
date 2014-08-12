using System;
using System.Collections.Generic;
using System.Linq;
using EntitiesCoreFramework.Data;
using FakeDbSet;

namespace CoreFrameworkTests
{
    public class MockDbSet<T> : InMemoryDbSet<T> where T : BaseEntity
    {
        private readonly HashSet<T> _data;

        public MockDbSet(IEnumerable<T> entities)
        {
            _data = new HashSet<T>(entities);

            foreach (var item in entities)
                this.Add(item);
        }

        public override T Find(params object[] keyValues)
        {
            return _data.SingleOrDefault(x => x.Id == (int)keyValues[0]);
        }
    }
}
