using System;

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
        int 
        Calc(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentOutOfRangeException(nameof(input));

            throw new System.NotImplementedException();
        }
    }
}
