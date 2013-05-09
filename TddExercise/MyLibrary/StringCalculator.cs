namespace MyLibrary
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    public class StringCalculator : IStringCalculator
    {
        public string GetSum(string input)
        {
            var numbers = ConvertToNumbers(input);

            EnsureTheNumbersAreAllValid(numbers);

            return SumTheNumbers(numbers);
        }

        private static List<int> ConvertToNumbers(string input)
        {
            var numbers = input.Split(',').Select(s => Convert.ToInt32(s)).ToList();
            return numbers;
        }

        private static string SumTheNumbers(IEnumerable<int> numbers)
        {
            var sum = numbers.Sum(i => i);
            return sum.ToString(CultureInfo.InvariantCulture);
        }

        private static void EnsureTheNumbersAreAllValid(List<int> numbers)
        {

            if (numbers.Any(i => i < 0 || i > 9))
                throw new InvalidInputException();
        }
    }
}