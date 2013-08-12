using System;
using System.Linq;
using DevExpress.Xpo;

namespace MainDemo.Module.Queries
{
    public interface IQueries
    {
        IQueries InTransaction { get; }
        IContactQueries Contacts { get; }
    }

    public class Queries : IQueries
    {       
        public Queries(Session session)
        {
            _Session = session;
        }

        private readonly Session _Session;
        private bool _InTransaction;

        public IQueries InTransaction
        {
            get
            {
                _InTransaction = true;
                return this;
            }
        }

        private IContactQueries _Contacts;
        public IContactQueries Contacts
        {
            get
            {
                if (_Contacts == null)
                    _Contacts = new ContactQueries(_Session, _InTransaction);
                return _Contacts;
            }
        }
    }
}