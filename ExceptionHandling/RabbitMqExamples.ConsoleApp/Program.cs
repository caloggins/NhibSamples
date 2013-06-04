namespace RabbitMqExamples.ConsoleApp
{
    using System;
    using System.Collections.Concurrent;
    using System.Threading.Tasks;

    using EasyNetQ;

    public class Program
    {
        private readonly IBus bus;
        private readonly Random random;
        private readonly BlockingCollection<Action<MyMessage>> workers = new BlockingCollection<Action<MyMessage>>();
        private int errorsThatOccurred;
        private int messagesProcessed;

        private Program(IBus bus, Random random)
        {
            this.bus = bus;
            this.random = random;
        }

        static void Main()
        {
            IBus bus = null;
            try
            {
                var random = new Random();
                bus = BusFactory.Create();

                var program = new Program(bus, random);
                program.Run();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
                Console.WriteLine("End simulation.");
                Console.ReadKey();

                if (bus != null)
                    bus.Dispose();
            }
        }

        public void Run()
        {
            SetupWorkers();

            PublishTestMessages();

            SubscribeToTheMessages();

            DisplayTheResults();
        }

        private void SubscribeToTheMessages()
        {
            Console.ReadKey();
            bus.Subscribe<MyMessage>(string.Empty, TaskTheMessages);
        }

        private void DisplayTheResults()
        {
            Console.ReadKey();
            Console.WriteLine();
            Console.WriteLine("Number of messages: {0}", messagesProcessed);
            Console.WriteLine("Number of errors: {0}", errorsThatOccurred);
        }

        private void PublishTestMessages()
        {
            for (var i = 0; i < 100; i++)
            {
                using (var channel = bus.OpenPublishChannel())
                    channel.Publish(new MyMessage { Id = Guid.NewGuid() });
            }
        }

        private void SetupWorkers()
        {
            for (var i = 0; i < 5; i++)
                workers.Add(message => Console.WriteLine("Message Id: {0}", message.Id));
        }

        private void TaskTheMessages(MyMessage message)
        {
            Task.Factory.StartNew(() => ProcessMessage(message))
            .ContinueWith(task =>
            {
                if (task.Exception == null)
                    return;

                using (var channel = bus.OpenPublishChannel())
                    channel.Publish(message);

                Console.WriteLine("*** An error occurred. ***");
                errorsThatOccurred++;
            });
        }

        private void ProcessMessage(MyMessage message)
        {
            messagesProcessed++;

            RandomlyThrowException();

            DoActualWork(message);
        }

        private void DoActualWork(MyMessage message)
        {
            var worker = workers.Take();

            try
            {
                worker(message);
            }
            finally
            {
                workers.Add(worker);
            }
        }

        private void RandomlyThrowException()
        {
            var next = random.Next(1, 10);
            if (next % 4 == 0)
                throw new InvalidOperationException();
        }
    }
}
