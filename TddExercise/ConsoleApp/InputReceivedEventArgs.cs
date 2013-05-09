namespace ConsoleApp
{
    using System;

    public class InputReceivedEventArgs : EventArgs
    {
        public InputReceivedEventArgs(string userInput)
        {
            UserInput = userInput;
        }
        
        public string UserInput { get; private set; }
    }
}