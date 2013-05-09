namespace ConsoleApp
{
    using System.Threading;
    using Castle.Core;
    using Castle.Windsor;
    using MyLibrary;

    public class Program : IInitializable
    {
        private readonly IDisplay display;
        private readonly IInputMonitor inputMonitor;
        private readonly IStringCalculator calculator;

        public Program(IDisplay display, IInputMonitor inputMonitor, IStringCalculator calculator)
        {
            this.display = display;
            this.inputMonitor = inputMonitor;
            this.calculator = calculator;
        }

        public static void Main(string[] args)
        {
            var container = new WindsorContainer();
            container.Install(new MyInstaller());

            var program = container.Resolve<Program>();
            program.Run();

            SpinWait.SpinUntil(() => false);
        }


        public void Run()
        {
            display.Write(@"Please enter a comma-separated list of numbers to add( ex. 1, 2, 3");
            inputMonitor.ReadLine();
        }

        public void Initialize()
        {
            inputMonitor.InputReceived += InputReceivedHandler;
        }

        private void InputReceivedHandler(object sender, InputReceivedEventArgs args)
        {
            try
            {
                var total = calculator.GetSum(args.UserInput);
                display.Write(string.Format("The sum of {0} is {1}.", args.UserInput, total));
            }
            catch (InvalidInputException)
            {
                display.Write("I didn't understand that, please try again.");
            }
        }
    }
}
