namespace TopShelfWcfExample.ConsoleApp
{
    using Castle.Core.Logging;

    public class ExampleService : IExampleService
    {
        private readonly ILogger logger;

        public ExampleService(ILogger logger)
        {
            this.logger = logger;
        }

        public void Start()
        {
            logger.Debug("The service was started.");
        }

        public void Stop()
        {
            logger.Debug("The service was stopped.");
        }
    }
}