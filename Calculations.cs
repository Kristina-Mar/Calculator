using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    internal class Calculations
    {
        public double _result;

        public void SetFirstNumber(double number1)
        {
            _result = number1;
        }

        public double Calculate(string symbol, double number2)
        {
            switch (symbol)
            {
                case "+":
                    _result += number2;
                    return _result;
                case "-":
                    _result -= number2;
                    return _result;
                case "*":
                    _result *= number2;
                    return _result;
                case "÷":
                    _result /= number2;
                    return _result;
                case "^":
                    _result = Math.Pow(_result, number2);
                    return _result;
                default: // This case won't happen.
                    return 0;
            }
        }
    }
}
