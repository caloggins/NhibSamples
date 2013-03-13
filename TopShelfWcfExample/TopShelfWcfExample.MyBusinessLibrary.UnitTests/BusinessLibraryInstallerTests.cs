namespace TopShelfWcfExample.MyBusinessLibrary.UnitTests
{
    using Castle.Windsor;
    using FluentAssertions;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class BusinessLibraryInstallerTests
    {
        [TestClass]
        public class WhenTheInstallerHasBeenUsed
        {
            [TestMethod]
            public void TheGreetingWithNameCommandShouldBeRegistered()
            {
                var container = new WindsorContainer();

                container.Install(SutFactory());

                var allTypes = WindsorTestHelpers.GetPublicClassesFromApplicationAssemblyContaining<GreetingWithNameCommand>(
                        type => type == typeof (GreetingWithNameCommand));
                var registeredTypes = container.GetImplementationTypesFor<GreetingWithNameCommand>();

                registeredTypes.Should().BeEquivalentTo(allTypes);
            }

            private BusinessLibraryInstaller SutFactory()
            {
                return new BusinessLibraryInstaller();
            }
        }
    }
}