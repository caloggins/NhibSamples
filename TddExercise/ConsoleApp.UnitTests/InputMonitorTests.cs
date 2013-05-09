namespace ConsoleApp.UnitTests
{
    using System;
    using System.IO;
    using System.Threading;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class InputMonitorTests
    {
        public class InputMonitorContext : ContextSpecification
        {
            protected InputMonitor Sut;

            protected override void Context()
            {
                base.Context();

                Sut = new InputMonitor();
            }
        }

        [TestClass]
        public class WhenInputIsReceived : InputMonitorContext
        {
            [TestMethod]
            public void ItShouldRaiseAnEvent()
            {
                InputReceivedEventArgs actualArgs = null;
                var expectedArgs = new InputReceivedEventArgs("testInput");
                var resetEvent = new AutoResetEvent(false);
                Sut.InputReceived += (sender, args) =>
                    {
                        actualArgs = args;
                        resetEvent.Set();
                    };

                using (var reader = new StringReader("testInput"))
                {
                    Console.SetIn(reader);
                    Sut.ReadLine();
                }

                if (!resetEvent.WaitOne(100))
                    Assert.Fail("The input event was never raised.");
                actualArgs.ShouldBeEquivalentTo(expectedArgs);
            }
        }
    }
}