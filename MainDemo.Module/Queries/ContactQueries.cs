using System;
using System.Linq;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using MainDemo.Module.BusinessObjects;

namespace MainDemo.Module.Queries
{
    public interface IContactQueries : IQueries<Contact>
    {
        IContactQueries ByDepartmentTitle(string departmentTitle);
        IContactQueries ByPosition(string position);
        Contact ByEmail(string email);
        IContactThatHaveQueries ThatHave { get; }
    }

    public interface IContactThatHaveQueries : IQueries<Contact>
    {
        IContactThatHaveQueries And { get; }
        IContactThatHaveQueries NoPhoto();
        IContactThatHaveQueries TasksWith(Priority priority);
        IContactThatHaveQueries TasksInProgress();
    }

    public class ContactQueries : Queries<Contact>, IContactQueries, IContactThatHaveQueries
    {
        public ContactQueries(Session session, bool inTransaction)
            : base(session, inTransaction)
        {
        }

        public IContactQueries ByDepartmentTitle(string department)
        {
            Query = Query.Where(p => p.Department.Title == department);
            return this;
        }

        public IContactQueries ByPosition(string position)
        {
            Query = Query.Where(p => p.Position.Title == position);
            return this;
        }

        public Contact ByEmail(string email)
        {
            return Query.SingleOrDefault(p => p.Email == email);
        }

        public IContactThatHaveQueries ThatHave
        {
            get
            {
                return this;
            }
        }

        public IContactThatHaveQueries TasksWith(Priority priority)
        {
            Query = Query.Where(p => p.Tasks.Any(t => t.Priority == priority));
            return this;
        }

        public IContactThatHaveQueries TasksInProgress()
        {
            Query = Query.Where(p => p.Tasks.Any(t => t.Status == TaskStatus.InProgress));
            return this;
        }

        public IContactThatHaveQueries And
        {
            get
            {
                return this;
            }
        }

        public IContactThatHaveQueries NoPhoto()
        {
            Query = Query.Where(p => p.Photo == null);
            return this;
        }
    }    
}
