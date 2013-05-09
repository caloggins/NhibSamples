namespace ConsoleApp.UnitTests
{
    using Castle.Core.Internal;
    using Castle.Windsor;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using MyLibrary;

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

        // ReSharper disable CoVariantArrayConversion
        [TestClass]
        public class ConfigurationSpecs : MyInstallerContext
        {
            private IWindsorContainer container;

            protected override void Context()
            {
                base.Context();

                container = new WindsorContainer();
            }

            protected override void BecauseOf()
            {
                container.Install(Sut);
            }

            [TestMethod]
            public void ItShouldRegisterTheMainProgram()
            {
                var classes = WindsorTestHelpers.GetPublicClassesFromApplicationAssembly<Program>(
                    type => type.Is<Program>());
                var handlers = container.GetImplementationTypesFor<Program>();

                classes.Should().NotBeEmpty();
                handlers.Should().BeEquivalentTo(classes);
            }

            [TestMethod]
            public void ItShouldRegisterADisplayDevice()
            {
                var classes = WindsorTestHelpers.GetPublicClassesFromApplicationAssembly<Program>(
                    type => type.Is<IDisplay>());
                var handlers = container.GetImplementationTypesFor<IDisplay>();

                classes.Should().NotBeEmpty();
                handlers.Should().BeEquivalentTo(classes);
            }

            [TestMethod]
            public void ItShouldRegisterACalculator()
            {
                var classes = WindsorTestHelpers.GetPublicClassesFromApplicationAssembly<IStringCalculator>(type => type.Is<IStringCalculator>());
                var handlers = container.GetImplementationTypesFor<IStringCalculator>();

                classes.Should().NotBeEmpty();
                handlers.Should().BeEquivalentTo(classes);
            }

            [TestMethod]
            public void ItShouldRegisterAnInputMonitor()
            {
                var classes = WindsorTestHelpers.GetPublicClassesFromApplicationAssembly<Program>(type => type.Is<IInputMonitor>());
                var handlers = container.GetImplementationTypesFor<IInputMonitor>();

                classes.Should().NotBeEmpty();
                handlers.Should().BeEquivalentTo(classes);
            }
        }
    }
}