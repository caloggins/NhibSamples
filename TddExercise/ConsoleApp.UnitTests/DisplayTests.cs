namespace ConsoleApp.UnitTests
{
    using System;
    using System.IO;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class DisplayTests
    {
        public class DisplayContext : ContextSpecification
        {
            protected Display Sut;

            protected override void Context()
            {
                base.Context();

                Sut = new Display();
            }
        }

        [TestClass]
        public class WhenGivenAString : DisplayContext
        {
            private StringWriter writer;
            private string sampleOutput;
            private string expecteOutput;
            private string actualOutput;

            protected override void Context()
            {
                base.Context();

                writer = new StringWriter();
                Console.SetOut(writer);

                sampleOutput = "sample message";
                expecteOutput = "sample message" + Environment.NewLine;
            }

            protected override void BecauseOf()
            {
                Sut.Write(sampleOutput);
                actualOutput = writer.ToString();
            }

            [TestMethod]
            public void ItShouldDisplayTheOutput()
            {
                actualOutput.Should().Be(expecteOutput);
            }

            protected override void Cleanup()
            {
                base.Cleanup();

                writer.Dispose();
            }
        }
    }
}