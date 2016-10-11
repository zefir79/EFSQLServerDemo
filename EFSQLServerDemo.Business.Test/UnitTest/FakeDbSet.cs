using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;

namespace EFSQLServerDemo.Business.Test.UnitTest
{
    public class FakeDbSet<T> : IDbSet<T> where T : class
    {
        private readonly Func<object[], IQueryable<T>, T> _finder;
        protected readonly HashSet<T> _data;
        protected readonly IQueryable<T> _query;

        public FakeDbSet()
        {
            _data = new HashSet<T>();
            _query = _data.AsQueryable();
        }

        public IQueryable<T> GetData()
        {
            return _query;
        }

        public FakeDbSet(Func<object[], IQueryable<T>, T> finder)
        {
            _finder = finder;
            _data = new HashSet<T>();
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            return _finder(keyValues, _query);
        }

        public T Add(T item)
        {
            if (item == null)
            {
                return null;
            }
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            if (item == null)
            {
                return null;
            }
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            if (item == null)
            {
                return null;
            }
            _data.Add(item);
            return item;
        }

        public T Create()
        {
            throw new NotImplementedException();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> Local
        {
            get { throw new NotImplementedException(); }
        }

        public void Detach(T item)
        {
            _data.Remove(item);
        }

        Type IQueryable.ElementType
        {
            get { return _query.ElementType; }
        }

        System.Linq.Expressions.Expression IQueryable.Expression
        {
            get { return _query.Expression; }
        }

        IQueryProvider IQueryable.Provider
        {
            get { return _query.Provider; }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
