namespace TopShelfWcfExample.MyBusinessLibrary
{
    using System;

    public class GreetingWithNameCommand
    {
        public string Name { get; set; }

        public virtual string GetGreeting()
        {
            if (string.IsNullOrWhiteSpace(Name))
                throw new InvalidOperationException();

            return string.Format("Hello, {0}.", Name);
        }
    }
}