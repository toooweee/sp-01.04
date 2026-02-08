using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace pz19
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a <see cref="Frame">.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private double? previousValue = null;
        private string pendingOperation = null;
        private bool newInput = true;

        public MainPage()
        {
            InitializeComponent();
        }

        private void NumberClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string digit = btn.Tag.ToString();

            if (newInput || displayText.Text == "Error")
            {
                displayText.Text = "";
                newInput = false;
            }

            if (digit == "." && displayText.Text.Contains(".")) return;

            displayText.Text += digit;
        }

        private void OperationClick(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            string op = btn.Tag.ToString();

            if (double.TryParse(displayText.Text, out double current))
            {
                if (previousValue.HasValue && pendingOperation != null)
                {
                    Calculate();
                }

                previousValue = current;
                pendingOperation = op;
                newInput = true;

                displayText.Text += " " + op + " ";
            }
        }

        private void EqualsClick(object sender, RoutedEventArgs e)
        {
            if (previousValue.HasValue && pendingOperation != null && double.TryParse(displayText.Text.Split(' ')[^1], out double current))
            {
                Calculate();
                previousValue = null;
                pendingOperation = null;
            }
        }

        private void ClearClick(object sender, RoutedEventArgs e)
        {
            displayText.Text = "";
            previousValue = null;
            pendingOperation = null;
            newInput = true;
        }

        private void Calculate()
        {
            if (!double.TryParse(displayText.Text.Split(' ')[^1], out double current)) return;

            try
            {
                double result = pendingOperation switch
                {
                    "+" => previousValue.Value + current,
                    "-" => previousValue.Value - current,
                    "*" => previousValue.Value * current,
                    "/" => current == 0 ? throw new DivideByZeroException() : previousValue.Value / current,
                    "^" => Math.Pow(previousValue.Value, current),
                    "√" => current < 0 ? throw new ArgumentException() : Math.Sqrt(current),
                    _ => current
                };

                displayText.Text = result.ToString();
                newInput = true;
            }
            catch
            {
                displayText.Text = "Error";
                newInput = true;
            }
        }
    }
}
