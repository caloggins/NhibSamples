namespace TopShelfWcfExample.ConsoleApp
{
    using System;
    using System.Diagnostics;

    public class Program
    {
        static void Main()
        {
            try
            {
            }
            catch (Exception exception)
            {
                var assemblyName = typeof(Program).AssemblyQualifiedName;

                if (!EventLog.SourceExists(assemblyName))
                    EventLog.CreateEventSource(assemblyName, "Application");

                var log = new EventLog { Source = assemblyName };
                log.WriteEntry(string.Format("{0}", exception), EventLogEntryType.Error);
            }
        }
    }
}
