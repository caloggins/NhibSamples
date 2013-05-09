namespace ConsoleApp
{
    using Castle.MicroKernel.Registration;
    using Castle.MicroKernel.SubSystems.Configuration;
    using Castle.Windsor;
    using MyLibrary;

    public class MyInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Component.For<Program>(),
                Component.For<IDisplay>().ImplementedBy<Display>(),
                Component.For<IInputMonitor>().ImplementedBy<InputMonitor>(),
                Component.For<IStringCalculator>().ImplementedBy<StringCalculator>()
                );
        }
    }
}