using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpo;
using Common;

namespace MainDemo.Module.Queries
{
    public interface IQueries<T> : IEnumerable<T>, IFluentInterface
    {
        T Single();
        T SingleOrDefault();
        T First();
        T FirstOrDefault();
    }

    public class Queries<T> : IQueries<T>
    {
        public Queries(Session session, bool inTransaction)
        {
            _Session = session;
            Query = new XPQuery<T>(session, inTransaction);
        }

        private readonly Session _Session;
        protected IQueryable<T> Query { get; set; }

        public IEnumerator<T> GetEnumerator()
        {
            return Query.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Query.GetEnumerator();
        }

        protected IQueryable<T1> CreateSubQuery<T1>()
        {
            return new XPQuery<T1>(_Session);
        }

        public T Single()
        {
            return Query.Single();
        }

        public T SingleOrDefault()
        {
            return Query.SingleOrDefault();
        }

        public T First()
        {
            return Query.First();
        }

        public T FirstOrDefault()
        {
            return Query.FirstOrDefault();
        }
    }
}
