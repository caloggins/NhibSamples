namespace TopShelfWcfExample.ConsoleApp
{
    using Castle.Core.Logging;
    using Castle.Facilities.WcfIntegration;
    using Castle.MicroKernel.Registration;
    using Castle.Windsor;
    using MyBusinessLibrary;

    public class GenericService : IGenericService
    {
        private readonly IWindsorContainer container;
        private readonly ILogger logger;

        public GenericService(IWindsorContainer container, ILogger logger)
        {
            this.container = container;
            this.logger = logger;
        }

        public void Start()
        {
            container.Register(
                Component.For<IWcfService>().ImplementedBy<MyWcfService>().AsWcfService(new DefaultServiceModel()),
                Component.For<GreetingWithNameCommand>()
                );

            logger.Debug("The service was started.");
        }

        public void Stop()
        {
            logger.Debug("The service was stopped.");
        }
    }
}