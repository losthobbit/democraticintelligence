﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace LostHobbit.Tests.Common
{
    /// <summary>
    /// In memory implementation of IDbSet for unit testing.
    /// </summary>
    /// <remarks>
    /// From http://romiller.com/2012/02/14/testing-with-a-fake-dbcontext/
    /// SO: In .NET Core there is no IDbSet, which this used to inherit from,
    /// so this class doesn't make sense anymore.  I need to figure out another
    /// way to fake a DbSet.
    /// </remarks>
    /// <typeparam name="T"></typeparam>
    public class FakeDbSet<T> : DbSet<T>, IQueryable<T>, IEnumerable<T>
        where T : class
    {
        protected ObservableCollection<T> _data;
        protected IQueryable _query;

        public FakeDbSet()
        {
            _data = new ObservableCollection<T>();
            _query = _data.AsQueryable();
        }

        public FakeDbSet(IEnumerable<T> items)
        {
            _data = new ObservableCollection<T>(items);
            _query = _data.AsQueryable();
        }

        public virtual T Find(params object[] keyValues)
        {
            throw new NotImplementedException("Derive from FakeDbSet<T> and override Find");
        }

        public T Add(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Remove(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Attach(T item)
        {
            _data.Add(item);
            return item;
        }

        public T Detach(T item)
        {
            _data.Remove(item);
            return item;
        }

        public T Create()
        {
            return Activator.CreateInstance<T>();
        }

        public TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, T
        {
            return Activator.CreateInstance<TDerivedEntity>();
        }

        public ObservableCollection<T> Local
        {
            get { return _data; }
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
