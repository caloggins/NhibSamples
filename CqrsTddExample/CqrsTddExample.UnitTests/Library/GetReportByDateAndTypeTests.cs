namespace CqrsTddExample.UnitTests.Library
{
    using System;
    using System.Collections.Generic;
    using CqrsTddExample.Library;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class GetReportByDateAndTypeTests
    {
        public class GetReportByDateAndTypeContext : ContextSpecification
        {
            protected GetReportByDateAndType Sut;

            protected override void Context()
            {
                Sut = new GetReportByDateAndType();
            }
        }

        [TestClass]
        public class WhenQueryingForAnExistingItem : GetReportByDateAndTypeContext
        {
            private DateTime sampleDateTime;
            private ReportType sampleReportType;
            private IEnumerable<Report> results;

            protected override void Context()
            {
                base.Context();

                sampleDateTime = new DateTime(2012, 01, 02);
                sampleReportType = ReportType.Cost;
            }

            protected override void BecauseOf()
            {
                Sut.ReportDate = sampleDateTime;
                Sut.ReportType = sampleReportType;
            }

            [TestMethod]
            public void ItShouldReturnTheExpectedItem()
            {
            }
        }
    }
}