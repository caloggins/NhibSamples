namespace CqrsTddExample.Library
{
    using System;
    using System.Linq;
    using NHibernate;

    public class GetExpenseReportsByDate : QueryBase
    {
        private readonly GetReportByDateAndType getReportByDateAndType;

        public GetExpenseReportsByDate(GetReportByDateAndType getReportByDateAndType)
        {
            this.getReportByDateAndType = getReportByDateAndType;
        }

        public DateTime ReportDate { get; set; }

        public override IQueryable<Report> Execute(ISession session)
        {
            getReportByDateAndType.ReportDate = ReportDate;
            getReportByDateAndType.ReportType = ReportType.Expense;

            return getReportByDateAndType.Execute(session);
        }
    }
}