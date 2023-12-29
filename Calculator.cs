using System.Diagnostics.Eventing.Reader;

namespace Calculator
{
    public partial class Calculator : Form
    {
        public Calculator()
        {
            InitializeComponent();
        }
        Calculations calculations = new Calculations();

        private bool inputtingNewNumber = true;
        private string symbol = string.Empty;
        private double secondNumber;

        private void Calculator_Shown(object sender, EventArgs e)
        {
            buttonEquals.Focus(); // Also after every step, possible to calculate by pressing Ent/space bar without defining a KeyDown method.
        }

        private void SetOperator(object sender) // Sets the mathematical symbol as defined on the button, power sign separate (x^y).
        {
            if (((Button)sender).Text == "x^y")
            {
                symbol = "^";
            }
            else
            {
                symbol = ((Button)sender).Text;
            }
        }
        
        private void AddDigitFromButton(object sender, EventArgs e) // Adds a digit from clicked buttons.
        {
            if (inputtingNewNumber || textBoxDisplay.Text == "0")
            {
                textBoxDisplay.Clear();
                inputtingNewNumber = false;
            }
            textBoxDisplay.Text += ((Button)sender).Text;
            buttonEquals.Focus();
        }

        private void buttonDecimal_Click(object sender, EventArgs e) // Adds a decimal point.
        {
            if (inputtingNewNumber || textBoxDisplay.Text == "0") // Different from numerals -> can begin with 0.
            {
                textBoxDisplay.Text = "0,";
                inputtingNewNumber = false;
            }
            else if (!textBoxDisplay.Text.Contains(",")) // Only one decimal point permitted in a number.
            {
                textBoxDisplay.Text += ",";
            }
            buttonEquals.Focus();
        }
        private void buttonOperator_Click(object sender, EventArgs e)
        {
            if (!inputtingNewNumber && symbol == string.Empty) // First pressing of mathematical symbol -> saves the first number and the symbol.
            {
                calculations._result = double.Parse(textBoxDisplay.Text);
                SetOperator(sender);
                textBoxCalculations.Text = calculations._result + " " + symbol;
                inputtingNewNumber = true;
                buttonEquals.Focus();
            }

            else if (!inputtingNewNumber) // Not the first pressing of a mathematical symbol button and the second number has been input -> calculation.
            {
                secondNumber = double.Parse(textBoxDisplay.Text);

                if (symbol == "÷" && secondNumber == 0)
                {
                    buttonReset.PerformClick();
                    textBoxDisplay.Text = "You cannot divide by 0!"; // Reset and a warning.
                }
                else
                {
                    calculations.Calculate(symbol, secondNumber);
                    textBoxDisplay.Text = calculations._result.ToString();
                    SetOperator(sender);
                    textBoxCalculations.Text = calculations._result + " " + symbol;
                    inputtingNewNumber = true;
                    buttonEquals.Focus();
                }
            }

            else // Not the first pressing of a mathematical symbol button and the second number has not been input -> changes the mathematical symbol.
            {
                SetOperator(sender);
                textBoxCalculations.Text = calculations._result + " " + symbol;
                inputtingNewNumber = true;
                buttonEquals.Focus();
            }
        }
        private void buttonReset_Click(object sender, EventArgs e) // Complete reset.
        {
            textBoxDisplay.Text = "0";
            textBoxCalculations.Text = string.Empty;
            inputtingNewNumber = true;
            calculations._result = 0;
            symbol = string.Empty;
            secondNumber = 0;
            buttonEquals.Focus();
        }

        private void buttonEquals_Click(object sender, EventArgs e)
        {
            if (!inputtingNewNumber) // Parses a new second number if it has been input.
                // Repeated pressing of = -> the second number stays the same and the calculation repeats.
            {
                secondNumber = double.Parse(textBoxDisplay.Text);
            }
            textBoxCalculations.Text = calculations._result + " " + symbol + " " + secondNumber + " =";
            if (symbol == "÷" && secondNumber == 0)
            {
                buttonReset.PerformClick();
                textBoxDisplay.Text = "You cannot divide by 0!"; // Reset and a warning.
            }
            else
            {
                calculations.Calculate(symbol, secondNumber);
                textBoxDisplay.Text = calculations._result.ToString();
                inputtingNewNumber = true;
                buttonEquals.Focus();
            }
        }

        private void buttonBackspace_Click(object sender, EventArgs e)
        {
            if (!inputtingNewNumber && textBoxDisplay.Text.Length >1)
                // Only possible to delete user's input, not the result.
            {
                textBoxDisplay.Text = textBoxDisplay.Text.Remove(textBoxDisplay.Text.Length - 1);
            }
            else if (!inputtingNewNumber)
            {
                textBoxDisplay.Text = "0";
            }
            else
            {
                textBoxCalculations.Text = string.Empty;
            }
        }

        private void Calculator_KeyDown(object sender, KeyEventArgs e)
        {
            // Input from numeric keypad.
            switch (e.KeyCode)
            {
                case Keys.NumPad0:
                    button0.PerformClick();
                    break;
                case Keys.NumPad1:
                    button1.PerformClick();
                    break;
                case Keys.NumPad2:
                    button2.PerformClick();
                    break;
                case Keys.NumPad3:
                    button3.PerformClick();
                    break;
                case Keys.NumPad4:
                    button4.PerformClick();
                    break;
                case Keys.NumPad5:
                    button5.PerformClick();
                    break;
                case Keys.NumPad6:
                    button6.PerformClick();
                    break;
                case Keys.NumPad7:
                    button7.PerformClick();
                    break;
                case Keys.NumPad8:
                    button8.PerformClick();
                    break;
                case Keys.NumPad9:
                    button9.PerformClick();
                    break;
                case Keys.Add:
                    buttonPlus.PerformClick();
                    break;
                case Keys.Subtract:
                    buttonMinus.PerformClick();
                    break;
                case Keys.Multiply:
                    buttonKrat.PerformClick();
                    break;
                case Keys.Divide:
                    buttonDeleno.PerformClick();
                    break;
                case Keys.Decimal:
                    buttonDesetinna.PerformClick();
                    break;
                case Keys.Back:
                    buttonBackspace.PerformClick();
                    break;
                default:
                    break;
            }
        }
    }
}
