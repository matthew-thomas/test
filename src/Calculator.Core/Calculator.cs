using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CoolCalc.Core
{
    public class Calculator
    {
        private readonly Dictionary<string, Func<double, double, double>> _supportedOperands;

        /// <summary>
        /// Initializes a new instance of Calculator with a default set of functions.
        /// </summary>
        public 
        Calculator()
        {
            _supportedOperands = new Dictionary<string, Func<double, double, double>> {
                {"+", (left, right) => left + right},
                {"-", (left, right) => left - right},
                {"*", (left, right) => left * right},
                {"/", (left, right) => left / right}
            };
        }

        /// <summary>
        /// Initializes a new instance of Calculator with supported functions.
        /// </summary>
        /// <param name="calcFunctions"></param>
        public
        Calculator(
            Dictionary<string, Func<double, double, double>> calcFunctions) : this()
        {
            foreach (var customFunc in calcFunctions)
            {
                _supportedOperands.Add(customFunc.Key, customFunc.Value);
            }
        }

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
            var orderedParts = CalulateHighestPrecendence(input);
            
            // Compute all
            return ComputeAll(orderedParts);
        }

        private 
        ArrayList 
        CalulateHighestPrecendence(string input)
        {
            var orderedParts = new ArrayList();
            var parts        = Regex.Split(input, "([-+*/])");

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
                        case "/":
                            orderedParts.Add(this._supportedOperands[nextOperator](number, nextValue));
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

            return orderedParts;
        }

        private 
        double 
        ComputeAll(ArrayList orderedParts)
        {
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
                        case "-":
                            answer = this._supportedOperands[nextOperator](number, nextValue);
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