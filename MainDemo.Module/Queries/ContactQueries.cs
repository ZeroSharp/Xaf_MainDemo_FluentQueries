using System;
using System.Linq;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using MainDemo.Module.BusinessObjects;
using System.Collections.Generic;

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
        IContactThatHaveQueries Resume();
        IContactThatHaveQueries TasksWith(Priority priority);
        IContactThatHaveQueries TasksInProgress();
    }

    public interface IResumeQueries : IQueries<Resume>
    {
    }

    public class ResumeQueries : Queries<Resume>, IResumeQueries
    {
        public ResumeQueries(Session session, bool inTransaction)
            : base(session, inTransaction)
        {
        }
    }

    public class ContactQueries : Queries<Contact>, IContactQueries, IContactThatHaveQueries
    {
        public ContactQueries(Session session, bool inTransaction)
            : base(session, inTransaction)
        {
            _Resumes = new ResumeQueries(session, inTransaction);
        }

        private ResumeQueries _Resumes;

        public IContactQueries ByDepartmentTitle(string department)
        {
            Query = Query.Where(c => c.Department.Title == department);
            return this;
        }

        public IContactQueries ByPosition(string position)
        {
            Query = Query.Where(c => c.Position.Title == position);
            return this;
        }

        public Contact ByEmail(string email)
        {
            return Query.SingleOrDefault(c => c.Email == email);
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
            Query = Query.Where(c => c.Tasks.Any(t => t.Priority == priority));
            return this;
        }

        public IContactThatHaveQueries TasksInProgress()
        {
            Query = Query.Where(c => c.Tasks.Any(t => t.Status == TaskStatus.InProgress));
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

        public IContactThatHaveQueries Resume()
        {
            //Query = Query.Where(c => _Resumes.Any(r => r.Contact == c));
            // Unforutnately the above results in 'Specified method is not supported' owing to a limitation of XPO to Linq

            var contactsWithResumes = _Resumes.Select(r => r.Contact).Distinct();
            Query = Query.Where(c => contactsWithResumes.Contains(c));

            return this;
        }
    }
}
