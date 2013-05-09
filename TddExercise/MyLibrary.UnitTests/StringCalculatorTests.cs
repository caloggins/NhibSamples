namespace MyLibrary.UnitTests
{
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class StringCalculatorTests
    {
        public class StringCalculatorContext : ContextSpecification
        {
            protected StringCalculator Sut;

            protected override void Context()
            {
                base.Context();

                Sut = new StringCalculator();
            }
        }

        [TestClass]
        public class WhenGivenAValidString : StringCalculatorContext
        {
            [TestMethod]
            internal void ItShouldHandleOneNumber()
            {
                const string sampleInput = "1";
                const string expectedOutput = "1";

                var actualOutput = Sut.GetSum(sampleInput);

                actualOutput.Should().Be(expectedOutput);
            }

            [TestMethod]
            public void ItShouldHandleTwoNumbers()
            {
                const string sampleInput = "1,2";
                const string expectedOutput = "3";

                var actualOutput = Sut.GetSum(sampleInput);

                actualOutput.Should().Be(expectedOutput);
            }

            [TestMethod]
            public void ItShouldHandleTwoOfTheSameNumber()
            {
                const string sampleInput = "9,9";
                const string expectedOutput = "18";

                var actualOutput = Sut.GetSum(sampleInput);

                actualOutput.Should().Be(expectedOutput);                
            }

            [TestMethod]
            public void ItShouldHandleASequenceWithAZero()
            {
                const string sampleInput = "0,1";
                const string expectedOutput = "1";

                var actualOutput = Sut.GetSum(sampleInput);

                actualOutput.Should().Be(expectedOutput);
            }

            [TestMethod]
            public void ItShouldHandleThreeNumbers()
            {
                const string sampleInput = "1,2,3";
                const string expectedOutput = "6";

                var actualOutput = Sut.GetSum(sampleInput);

                actualOutput.Should().Be(expectedOutput);
            }
        }

        [TestClass]
        public class WhenGivenAnInvalidString : StringCalculatorContext
        {
            [TestMethod]
            public void ItShouldReturnAnException()
            {
                const string sampleInput = "8,-1";

                var exception = Capture.Exception(() => Sut.GetSum(sampleInput));

                exception.Should().BeOfType<InvalidInputException>();
            }
        }
    }
}