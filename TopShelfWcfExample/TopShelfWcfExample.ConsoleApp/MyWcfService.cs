namespace TopShelfWcfExample.ConsoleApp
{
    using MyBusinessLibrary;

    public class MyWcfService : IWcfService
    {
        private readonly GreetingWithNameCommand greetingWithNameCommand;

        public MyWcfService(GreetingWithNameCommand greetingWithNameCommand)
        {
            this.greetingWithNameCommand = greetingWithNameCommand;
        }

        public string Greet()
        {
            return "Hello, world.";
        }

        public string GetGrettingWithName(string name)
        {
            greetingWithNameCommand.Name = name;
            return greetingWithNameCommand.GetGreeting();
        }
    }
}