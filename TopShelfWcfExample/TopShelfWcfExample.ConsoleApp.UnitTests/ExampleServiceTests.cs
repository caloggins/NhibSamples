namespace TopShelfWcfExample.ConsoleApp.UnitTests
{
    using Castle.Core.Logging;
    using FakeItEasy;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class ExampleServiceTests
    {
        public class ExampleServiceContext : ContextSpecification
        {
            protected ExampleService Sut;
            protected ILogger Logger;

            protected override void Context()
            {
                base.Context();

                Logger = A.Fake<ILogger>();
                Sut = new ExampleService(Logger);
            }
        }

        [TestClass]
        public class WhenTheServiceHasBeenStarted : ExampleServiceContext
        {
            private string loggedMessage;

            protected override void Context()
            {
                base.Context();

                A.CallTo(() => Logger.Debug(A<string>.Ignored))
                 .Invokes(call => loggedMessage = (string) call.Arguments[0]);
            }

            protected override void BecauseOf()
            {
                Sut.Start();
            }

            [TestMethod]
            public void ItShouldLogAMessage()
            {
                loggedMessage.Should().Be("The service was started.");
            }
        }

        [TestClass]
        public class WhenTheServiceIsStopped : ExampleServiceContext
        {
            private string loggedMessage;

            protected override void Context()
            {
                base.Context();

                A.CallTo(() => Logger.Debug(A<string>.Ignored))
                 .Invokes(call => loggedMessage = (string)call.Arguments[0]);
            }

            protected override void BecauseOf()
            {
                Sut.Stop();
            }

            [TestMethod]
            public void ItShouldLogAMessage()
            {
                loggedMessage.Should().Be("The service was stopped.");
            }
        }
    }
}