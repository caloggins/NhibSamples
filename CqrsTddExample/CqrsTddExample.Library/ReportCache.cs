namespace CqrsTddExample.Library
{
    using System;
    using System.Linq;
    using NHibernate;

    public class ReportCache
    {
        private readonly ISession session;
        private readonly GetReportByDateAndType getReportByDateAndType;

        public ReportCache(ISession session, GetReportByDateAndType getReportByDateAndType)
        {
            this.session = session;
            this.getReportByDateAndType = getReportByDateAndType;
        }

        public Report GetReportByDateAndType(DateTime reportDate, ReportType reportType)
        {
            getReportByDateAndType.ReportDate = reportDate;
            getReportByDateAndType.ReportType = reportType;
            var reports = getReportByDateAndType.Execute(session);

            return reports.First();
        }
    }
}