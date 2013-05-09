namespace ConsoleApp
{
    using System;

    public class Display : IDisplay
    {
        public void Write(string output)
        {
            Console.WriteLine(output);
        }
    }
}