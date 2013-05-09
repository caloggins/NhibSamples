namespace ConsoleApp.UnitTests
{
    using Castle.Core;
    using FakeItEasy;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyLibrary;

    public static class ProgramTests
    {
        public class ProgramContext : ContextSpecification
        {
            protected Program Sut;
            protected IDisplay Display;
            protected IInputMonitor InputMonitor;
            protected IStringCalculator Calculator;

            protected override void Context()
            {
                base.Context();

                Display = A.Fake<IDisplay>();
                InputMonitor = A.Fake<IInputMonitor>();
                Calculator = A.Fake<IStringCalculator>();

                Sut = new Program(Display, InputMonitor, Calculator);
                Sut.Initialize();
            }
        }

        [TestClass]
        public class ContractSpecs : ProgramContext
        {
            [TestMethod]
            public void ItShouldBeInitializable()
            {
                Sut.Should().BeAssignableTo<IInitializable>();
            }
        }

        [TestClass]
        public class WhenStarted : ProgramContext
        {
            protected override void BecauseOf()
            {
                Sut.Run();
            }

            [TestMethod]
            public void ItShouldPromptTheUserForInput()
            {
                A.CallTo(() => Display.Write("Please enter a comma-separated list of numbers to add( ex. 1, 2, 3"))
                 .MustHaveHappened(Repeated.Exactly.Once);
            }

            [TestMethod]
            public void ItShouldWaitForUserInput()
            {
                A.CallTo(() => InputMonitor.ReadLine())
                 .MustHaveHappened(Repeated.Exactly.Once);
            }
        }

        [TestClass]
        public class WhenInputIsReceivedFromTheUser : ProgramContext
        {
            private string testInput;
            private InputReceivedEventArgs testEventArgs;

            protected override void Context()
            {
                base.Context();

                testInput = "input";
                testEventArgs = new InputReceivedEventArgs(testInput);

                const string sampleSum = "42";
                A.CallTo(() => Calculator.GetSum(A<string>._))
                 .Returns(sampleSum);
            }

            protected override void BecauseOf()
            {
                InputMonitor.InputReceived += Raise.With(testEventArgs).Now;
            }

            [TestMethod]
            public void ItShouldPassTheInputToTheCalculator()
            {
                A.CallTo(() => Calculator.GetSum(testInput))
                 .MustHaveHappened(Repeated.Exactly.Once);
            }

            [TestMethod]
            public void ItShouldDeplayTheTotalOfTheList()
            {
                A.CallTo(() => Display.Write(@"The sum of input is 42."))
                 .MustHaveHappened(Repeated.Exactly.Once);
            }
        }

        [TestClass]
        public class WhenTheInputIsInvalid : ProgramContext
        {
            private string testInput;
            private InputReceivedEventArgs testEventArgs;

            protected override void Context()
            {
                base.Context();

                testInput = "input";
                testEventArgs = new InputReceivedEventArgs(testInput);

                A.CallTo(() => Calculator.GetSum(A<string>._))
                 .Throws(new InvalidInputException());
            }

            protected override void BecauseOf()
            {
                InputMonitor.InputReceived += Raise.With(testEventArgs).Now;
            }

            [TestMethod]
            public void ItShouldDisplayAMessage()
            {
                A.CallTo(() => Display.Write(@"I didn't understand that, please try again."))
                 .MustHaveHappened(Repeated.Exactly.Once);
            }
        }
    }
}