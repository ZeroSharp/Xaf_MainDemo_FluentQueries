using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace MainDemo.Module.Queries
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
        public static IQueries Query(this Session session)
        {
            return new Queries(session);
        }

        public static IQueries Query(this IObjectSpace objectSpace)
        {
            return new Queries(objectSpace.GetSession());
        }
    }
}
