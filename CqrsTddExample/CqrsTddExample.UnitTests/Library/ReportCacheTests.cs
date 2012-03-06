namespace CqrsTddExample.UnitTests.Library
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using CqrsTddExample.Library;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using NHibernate;
    using NSubstitute;

    public static class ReportCacheTests
    {
        public class ReportCacheSpecs : ContextSpecification
        {
            protected ReportCache Sut;
            protected GetReportByDateAndType TestQuery;
            protected ISession TestSession;

            protected override void Context()
            {
                TestSession = Substitute.For<ISession>();
                TestQuery = Substitute.For<GetReportByDateAndType>();
                Sut = new ReportCache(TestSession, TestQuery);
            }
        }

        [TestClass]
        public class MockWithNSubstitute : ReportCacheSpecs
        {
            private Report expectedReport;
            private Report result;
            private DateTime testDate;
            private ReportType testReportType;

            protected override void Context()
            {
                base.Context();

                testDate = new DateTime(2012, 01, 02);
                testReportType = ReportType.Cost;

                expectedReport = new Report
                                     {
                                         ReportId = 42L,
                                     };
                TestQuery.Execute(TestSession)
                    .Returns(new List<Report> { expectedReport }.AsQueryable());
            }

            protected override void BecauseOf()
            {
                result = Sut.GetReportByDateAndType(testDate, testReportType);
            }

            [TestMethod]
            public void TheCacheShouldReturnTheFirstReport()
            {
                result.Should().Be(expectedReport);
            }

            [TestMethod]
            public void TheCorrectParametersShouldBeSetOnTheQuery()
            {
                TestQuery.ReportDate.Should().Be(testDate);
                TestQuery.ReportType.Should().Be(testReportType);
            }
        }
    }
}