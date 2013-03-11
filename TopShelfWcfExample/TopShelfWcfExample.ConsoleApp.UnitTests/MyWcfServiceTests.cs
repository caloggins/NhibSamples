namespace TopShelfWcfExample.ConsoleApp.UnitTests
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class MyWcfServiceTests
    {
        public class MyWcfServiceContext : ContextSpecification
        {
            protected MyWcfService Sut;

            protected override void Context()
            {
                base.Context();

                Sut = new MyWcfService();
            }
        }

        [TestClass]
        public class WhenTheServiceIsCalled : MyWcfServiceContext
        {
            private string response;
            private const string ExpectedMessage = @"Hello, world.";

            protected override void BecauseOf()
            {
                response = Sut.Greet();
            }

            [TestMethod]
            public void ItShouldRetunrAGreeting()
            {
                response.Should().Be(ExpectedMessage);
            }
        }
    }
}