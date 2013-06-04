namespace RabbitMqExamples.ConsoleApp
{
    using System;
    using System.Configuration;

    using EasyNetQ;

    public static class BusFactory
    {
        public static IBus Create()
        {
            var settings = ConfigurationManager.ConnectionStrings["rabbit"];

            if (settings == null || string.IsNullOrEmpty(settings.ConnectionString))
                throw new InvalidOperationException();

            return RabbitHutch.CreateBus(settings.ConnectionString);
        }
    }
}