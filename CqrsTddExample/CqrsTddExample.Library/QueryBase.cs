namespace CqrsTddExample.Library
{
    using System.Linq;
    using NHibernate;

    public abstract class QueryBase
    {
        public abstract IQueryable<Report> Execute(ISession session);
    }
}