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

    public static class GetExpenseReportsByDateTests
    {
        public class GetExpenseReportsByDateSpecs : ContextSpecification
        {
            protected GetExpenseReportsByDate Sut;
            protected ISession TestSession;
            protected GetReportByDateAndType TestBaseQuery;

            protected override void Context()
            {
                TestSession = Substitute.For<ISession>();
                TestBaseQuery = Substitute.For<GetReportByDateAndType>();
                Sut = new GetExpenseReportsByDate(TestBaseQuery);
            }
        }

        [TestClass]
        public class WhenTheQueryIsExecuted : GetExpenseReportsByDateSpecs
        {
            private Report expectedContent;
            private IQueryable<Report> results;
            private DateTime testDate;

            protected override void Context()
            {
                base.Context();

                expectedContent = new Report {ReportId = 42L};

                TestBaseQuery.Execute(TestSession)
                    .Returns(new List<Report> {expectedContent}.AsQueryable());

                testDate = new DateTime(2012, 02, 03);
            }

            protected override void BecauseOf()
            {
                Sut.ReportDate = testDate;
                results = Sut.Execute(TestSession);
            }

            [TestMethod]
            public void ItShouldReturnTheResults()
            {
                results.Should().OnlyContain(report => report.Equals(expectedContent));
            }

            [TestMethod]
            public void TheBaseQueryShouldHaveTheCorrectValues()
            {
                TestBaseQuery.ReportDate.Should().Be(testDate);
                TestBaseQuery.ReportType.Should().Be(ReportType.Expense);
            }
        }
    }
}