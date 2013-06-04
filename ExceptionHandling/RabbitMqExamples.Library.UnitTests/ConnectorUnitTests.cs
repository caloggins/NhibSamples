namespace RabbitMqExamples.Library.UnitTests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class ConnectorTests
    {
        public class ConnectorContext : ContextSpecification
        {
            protected Connector Sut;

            protected override void Context()
            {
                base.Context();

                Sut = new Connector();
            }
        }

        [TestClass]
        public class WhenTheBusClosesAfterASubscriptionIsCreated : ConnectorContext
        {
            [TestMethod]
            public void ItShouldLogAnException()
            {
                Assert.Inconclusive("Test not yet implemented.");
            }
        }
    }

    public class Connector
    {
    }
}