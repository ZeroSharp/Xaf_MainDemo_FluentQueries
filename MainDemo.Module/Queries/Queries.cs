using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base.General;

namespace MainDemo.Module.BusinessObjects
{
    public static class ObjectSpaceExtensions
    {
        public static Session GetSession(this IObjectSpace objectSpace)
        {
            var xpObjectSpace = objectSpace as XPObjectSpace;
            if (xpObjectSpace != null)
                return xpObjectSpace.Session;
            return null;
        }
    }

    public static class QueryExtensions
    {
        public static IQueries Query(this Session session, bool inTransaction = false)
        {
            return new Queries(session, inTransaction);
        }

        public static IQueries Query(this IObjectSpace objectSpace, bool inTransaction = false)
        {
            return new Queries(objectSpace.GetSession(), inTransaction);
        }
    }

    public class Queries : IQueries
    {
        public Queries(Session session, bool inTransaction)
        {
            _Session = session;

            Contacts = new ContactQuery(session, inTransaction);
        }

        private readonly Session _Session;
        public IContactQuery Contacts { get; private set; }
    }

    public interface IQueries : IFluentInterface
    {
        IContactQuery Contacts { get; }
        //ITaskQuery Tasks { get; }
    }

    public interface IQuery<T> : IEnumerable<T>, IFluentInterface
    {
        T Single();
        T SingleOrDefault();
        T First();
        T FirstOrDefault();
    }

    public interface IContactQuery : IQuery<Contact>
    {
        IContactQuery ByDepartment(Department department);
        Contact ByEmail(string email);
        IContactQuery WithTasksInProgress();
    }

    public class QueryBase<T> : IQuery<T>
    {
        public QueryBase(Session session, bool inTransaction)
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

    public class ContactQuery : QueryBase<Contact>, IContactQuery
    {
        public ContactQuery(Session session, bool inTransaction) 
            : base(session, inTransaction)
        {
        }

        public IContactQuery ByDepartment(Department department)
        {
            Query = Query.Where(p => p.Department == department);
            return this;
        }

        public Contact ByEmail(string email)
        {
            return Query.SingleOrDefault(p => p.Email == email);
        }

        public IContactQuery WithTasksInProgress()
        {
            Query = Query.Where(p => p.Tasks.Any(t => t.Status == TaskStatus.InProgress));
            return this;
        }
    }
}
