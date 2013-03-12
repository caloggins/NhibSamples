namespace TopShelfWcfExample.ConsoleApp
{
    public class MyWcfService : IWcfService
    {
        public string Greet()
        {
            return "Hello, world.";
        }
    }
}