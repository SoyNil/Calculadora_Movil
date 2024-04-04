using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Calculadora
{
    public partial class MainPage : ContentPage
    {
        private string currentInput = "";
        private double result = 0;
        private string currentOperator = "";
        private double firstNumber = double.NaN;
        public MainPage()
        {
            InitializeComponent();
        }
        private bool isResultDisplayed = false;
        private void UpdateUI()
        {
            if (!string.IsNullOrEmpty(currentInput) || !double.IsNaN(firstNumber))
            {
                if (isResultDisplayed)
                {
                    spnFirst.Text = "";
                    spnSecond.Text = "";
                    spnThird.Text = "";
                }
                else
                {
                    spnFirst.Text = double.IsNaN(firstNumber) ? "" : firstNumber.ToString();
                    spnSecond.Text = currentOperator;
                    spnThird.Text = currentInput;
                }
            }
            else
            {
                spnFirst.Text = "";
                spnSecond.Text = "";
                spnThird.Text = "";
            }

            lblNumber.Text = isResultDisplayed ? result.ToString() : currentInput;
        }
        private void Btn_sumar(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            string buttonText = button.Text;
            switch (buttonText)
            {
                case "AC":
                    currentInput = "";
                    firstNumber = double.NaN;
                    currentOperator = "";
                    isResultDisplayed = false;
                    break;
                case "=":
                    if (!string.IsNullOrEmpty(currentInput) && !string.IsNullOrEmpty(currentOperator))
                    {
                        double num;
                        if (double.TryParse(currentInput, out num))
                        {
                            switch (currentOperator)
                            {
                                case "+":
                                    result = firstNumber + num;
                                    break;
                                case "-":
                                    result = firstNumber - num;
                                    break;
                                case "*":
                                    result = firstNumber * num;
                                    break;
                                case "/":
                                    if (num != 0)
                                    {
                                        result = firstNumber / num;
                                    }
                                    else
                                    {
                                        lblNumber.Text = "No se puede dividir entre cero";
                                        return;
                                    }
                                    break;
                            }
                            currentInput = result.ToString();
                            firstNumber = result;
                            currentOperator = "";
                            isResultDisplayed = true;
                        }
                    }
                    break;
                case "←":
                    if (!string.IsNullOrEmpty(currentInput))
                    {
                        currentInput = currentInput.Remove(currentInput.Length - 1);
                    }
                    break;
                default:
                    if (buttonText == "+" || buttonText == "-" || buttonText == "*" || buttonText == "/")
                    {
                        if (!string.IsNullOrEmpty(currentInput))
                        {
                            firstNumber = double.Parse(currentInput);
                        }
                        currentOperator = buttonText;
                        currentInput = "";
                        isResultDisplayed = false;
                    }
                    else
                    {
                        currentInput += buttonText;
                    }
                    break;
            }
            UpdateUI();
        }
    }
}

