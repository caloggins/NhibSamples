namespace CqrsTddExample.Library
{
    using System;

    public class Report
    {
        public virtual long ReportId { get; set; }
        public virtual DateTime ReportDate { get; set; }
        public virtual ReportType Type { get; set; }
        public virtual string Contents { get; set; }
    }
}