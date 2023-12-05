using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq.Expressions;

namespace MauiApp2
{
    public partial class MainPage : ContentPage
    {
        
        public MainPage()
        {
            InitializeComponent();

            var buttons = new Button[] { button1, button2, button3, button4, button5, button6, button7, button8, button9, button10, button11, button12, button13, button14, button15, button16, button17, button18, button19, button20 };

            foreach (var button in buttons)
            {
                button.Clicked += OnButtonClicked;
            }
        }
        int countBrackets = 0;
        bool isDotPresent = false;

        private void OnButtonClicked(object sender, EventArgs e)
        {
            var button = sender as Button;
            int lastElement = display.Text.Length - 1;

            if (display.Text == "0" && !char.IsDigit(button.Text[lastElement]) && button.Text != "()" )
            {
                return;
            } else if (button.Text == "()" && display.Text == "0")
            {
                display.Text = "(";
                countBrackets++;
                return;
            }

            if (button.Text == "C")
            {
                display.Text = "0";
                solution.Text = "0";

                isDotPresent = false;
                countBrackets = 0;
            }
            else if (char.IsDigit(button.Text[0]))
            {
                if (display.Text == "0")
                    display.Text = button.Text;
                else if (display.Text[lastElement] != ')')
                {
                    display.Text += button.Text;
                }
            }
            else if (button.Text == "()")
            {
                if ((char.IsDigit(display.Text[lastElement]) || display.Text[lastElement] == ')') && countBrackets > 0)
                {
                    display.Text += ")";
                    countBrackets--;
                    isDotPresent = false;
                }
                else if (!char.IsDigit(display.Text[lastElement]) && display.Text[lastElement] != ')' && display.Text[lastElement] != '.')
                {
                    display.Text += "(";
                    countBrackets++;
                    isDotPresent = false;
                }
            }
            else if (button.Text == ".")
            {
                if (!isDotPresent && char.IsDigit(display.Text[lastElement]))
                {
                    display.Text += ".";
                    isDotPresent = true;
                }
            }
            else if (button.Text == "⌫")
            {
                if (display.Text.Length == 0)
                    return;

                if (display.Text[lastElement] == '(')
                {
                    countBrackets--;
                } else if (display.Text[lastElement] == ')')
                {
                    countBrackets++;
                }

                // Remove the last character
                display.Text = display.Text.Remove(display.Text.Length - 1);

                if (display.Text.Length == 0)
                    display.Text = "0";
            }
            else if (button.Text == "=")
            {
                CalculateDisplay();
            }
            else
            {
                if (char.IsDigit(display.Text[lastElement]) || display.Text[lastElement] == ')')
                {
                    display.Text += button.Text;
                    isDotPresent = false;
                }
            }
        }
        private void CalculateDisplay()
        {
            string mathExpression = display.Text;

            try
            {
                DataTable table = new DataTable();
                DataColumn column = new DataColumn("Result", typeof(double), mathExpression);
                table.Columns.Add(column);
                DataRow row = table.NewRow();
                table.Rows.Add(row);

                // Retrieve the result
                double result = (double)row["Result"];

                // Assuming result is a number, convert it to string and update the solution
                solution.Text = result.ToString();
            }
            catch (Exception ex)
            {
                // Handle errors
                if (ex.Message.Contains("Cannot perform 'Mod' operation"))
                {
                    solution.Text = "Cannot perform Mod on decimal numbers";
                }
                else
                {
                    // Handle other exceptions
                    solution.Text = ex.Message;
                }
            }
        }
    }
}