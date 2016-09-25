using System;
using System.Collections;
using System.Text.RegularExpressions;

namespace CoolCalc.Core
{
    public class Calculator
    {
        /// <summary>
        /// Calculate the input string.
        /// </summary>
        /// <param name="input">A mathematical expression to compute.</param>
        /// <returns>Returns the result of the computation.</returns>
        public 
        double 
        Calc(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentOutOfRangeException(nameof(input));

            // Split expression in to numeral/operator parts.
            var parts = Regex.Split(input, "([-+*/])");

            var orderedParts = new ArrayList();

            // Compute higher precedence operations on first pass
            for (var i = 0; i < parts.Length - 1; i++)
            {
                var currentValue = parts[i];

                double number;
                var isNumeric = double.TryParse(currentValue, out number);

                if (isNumeric)
                {
                    // Check what operator is next
                    var nextOperator = parts[i + 1];
                    var nextValue = long.Parse(parts[i + 2]);

                    switch (nextOperator)
                    {
                        case "*":
                            orderedParts.Add(number*nextValue);
                            break;
                        case "/":
                            orderedParts.Add(number/nextValue);
                            break;
                        case "+":
                        case "-":
                            orderedParts.Add(currentValue);
                            orderedParts.Add(nextOperator);
                            orderedParts.Add(nextValue);
                            break;
                    }

                    // Skip to next set.
                    i += 2;
                }
                else
                {
                    orderedParts.Add(currentValue);
                }
            }

            // Compute all
            var answer = 0D;

            for (var i = 0; i < orderedParts.Count - 1; i++)
            {
                var currentValue = orderedParts[i];

                double number;
                var isNumeric = double.TryParse(currentValue.ToString(), out number);

                if (isNumeric)
                {
                    var nextOperator = orderedParts[i + 1].ToString();
                    var nextValue = double.Parse(orderedParts[i + 2].ToString());

                    switch (nextOperator)
                    {
                        case "+":
                            answer = number + nextValue;
                            break;
                        case "-":
                            answer = number - nextValue;
                            break;
                    }
                }

                // Skip to next set.
                i += 2;
            }
            
            return answer;
        }
    }
}
