namespace TopShelfWcfExample.MyBusinessLibrary.UnitTests
{
    using System;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class GreetingWithNameCommandTests
    {
        public class GreetingWithNameCommandContext : ContextSpecification
        {
            protected GreetingWithNameCommand Sut;

            protected override void Context()
            {
                base.Context();

                Sut = new GreetingWithNameCommand();
            }
        }

        [TestClass]
        public class WhenTheNameIsInvalid : GreetingWithNameCommandContext
        {
            private Exception thrownException;

            protected override void BecauseOf()
            {
                thrownException = Capture.Exception(() => Sut.GetGreeting());
            }

            [TestMethod]
            public void AnExceptionShouldBeThrown()
            {
                thrownException.Should().BeOfType<InvalidOperationException>();
            }
        }

        [TestClass]
        public class WhenGivenAValidName : GreetingWithNameCommandContext
        {
            private string returnedGreeting;
            private const string SampleName = @"Chris";
            private const string ExpectedGreeting = @"Hello, Chris.";

            protected override void BecauseOf()
            {
                Sut.Name = SampleName;
                returnedGreeting = Sut.GetGreeting();
            }

            [TestMethod]
            public void ItShouldReturnTheExpectedGreeting()
            {
                returnedGreeting.Should().Be(ExpectedGreeting);
            }
        }
    }
}