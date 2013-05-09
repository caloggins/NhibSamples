namespace ConsoleApp
{
    using System;

    public interface IInputMonitor
    {
        event EventHandler<InputReceivedEventArgs> InputReceived;
        void ReadLine();
    }
}