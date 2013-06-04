namespace RabbitMqExamples.ConsoleApp
{
    using System;
    using System.Threading;
    using EasyNetQ;

    public class Connector
    {
        private readonly IBus bus;

        public Connector(IBus bus)
        {
            this.bus = bus;
        }

        public void Run()
        {
            Console.WriteLine("Starting connection");

            StartSubscription();

            PublishSampleMessage();

            EndTheTest();
        }

        private void EndTheTest()
        {
            Console.WriteLine("Press <esc> to exit.");
            SpinWait.SpinUntil(() => Console.ReadKey().Key == ConsoleKey.Escape);
        }

        private void PublishSampleMessage()
        {
            using (var channel = bus.OpenPublishChannel())
                channel.Publish(new Pebble {Message = "Hello, world."});
            Console.WriteLine("Received message, press <space> to continue.");
            SpinWait.SpinUntil(() => Console.ReadKey().Key == ConsoleKey.Spacebar);
        }

        private void StartSubscription()
        {
            bus.Subscribe<Pebble>("test", pebble => Console.WriteLine(pebble.Message));
            Console.WriteLine("Subscribed to message, press <space> to continue.");
            SpinWait.SpinUntil(() => Console.ReadKey().Key == ConsoleKey.Spacebar);
        }
    }

    public class Pebble
    {
        public string Message { get; set; }
    }
}