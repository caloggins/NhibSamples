namespace TopShelfWcfExample.ConsoleApp.UnitTests
{
    using System;
    using System.Linq;
    using Castle.Core.Logging;
    using Castle.Windsor;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class MyInstallerTests
    {
        public class MyInstallerContext : ContextSpecification
        {
            protected MyInstaller Sut;

            protected override void Context()
            {
                base.Context();

                Sut = new MyInstaller();
            }
        }

        [TestClass]
        public class WhenTheContainerIsInstalled : MyInstallerContext
        {
            private IWindsorContainer container;

            protected override void BecauseOf()
            {
                container = new WindsorContainer()
                    .Install(Sut);
            }

            [TestMethod]
            public void ItShouldRegisterTheService()
            {
                container.GetImplementationTypesFor<IExampleService>().Should().HaveCount(1);
            }
        }
    }
}