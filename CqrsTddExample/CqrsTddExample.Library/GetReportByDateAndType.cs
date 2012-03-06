namespace CqrsTddExample.Library
{
    using System;
    using System.Linq;
    using NHibernate;
    using NHibernate.Linq;

    public class GetReportByDateAndType : QueryBase
    {
        public DateTime ReportDate { get; set; }
        public ReportType ReportType { get; set; }

        public override IQueryable<Report> Execute(ISession session)
        {
            return session.Query<Report>()
                .Where(report => report.ReportDate == ReportDate && report.Type == ReportType);
        }
    }
}