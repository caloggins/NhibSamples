namespace ConsoleApp
{
    using System;

    public class InputMonitor : IInputMonitor
    {
        public event EventHandler<InputReceivedEventArgs> InputReceived;
        protected virtual void OnInputReceived(string input)
        {
            var e = InputReceived;
            if (e != null)
                e(this, new InputReceivedEventArgs(input));
        }

        public void ReadLine()
        {
            var input = Console.ReadLine();
            OnInputReceived(input);
        }
    }
}