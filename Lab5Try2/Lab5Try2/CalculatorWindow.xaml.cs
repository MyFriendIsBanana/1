using Lab5Try2;
using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfLab5
{
    public partial class CalculatorWindow : Window
    {
        private double _lastNumber = 0;
        private string _operation = "";
        private bool _isNewNumber = true;

        public CalculatorWindow()
        {
            InitializeComponent();
            txtCurrent.Text = "0";
        }

        private void InitializeComponent()
        {
            throw new NotImplementedException();
        }

        private void BtnDigit_Click(object sender, RoutedEventArgs e)
        {
            var digit = (sender as Button).Tag.ToString();
            if (_isNewNumber)
            {
                txtCurrent.Text = digit;
                _isNewNumber = false;
            }
            else
            {
                if (txtCurrent.Text == "0") txtCurrent.Text = digit;
                else txtCurrent.Text += digit;
            }
        }

        private void BtnDecimal_Click(object sender, RoutedEventArgs e)
        {
            if (!_isNewNumber && !txtCurrent.Text.Contains("."))
                txtCurrent.Text += ".";
            else if (_isNewNumber)
            {
                txtCurrent.Text = "0.";
                _isNewNumber = false;
            }
        }

        private void BtnOperator_Click(object sender, RoutedEventArgs e)
        {
            var op = (sender as Button).Tag.ToString();
            if (!_isNewNumber)
            {
                BtnEquals_Click(null, null); // обчислити попереднє
            }
            _lastNumber = double.Parse(txtCurrent.Text);
            _operation = op;
            _isNewNumber = true;
            txtHistory.Text = _lastNumber + " " + op;
        }

        private void BtnEquals_Click(object sender, RoutedEventArgs e)
        {
            if (_operation == "" || _isNewNumber) return;

            double current = double.Parse(txtCurrent.Text);
            double result = 0;

            switch (_operation)
            {
                case "+": result = _lastNumber + current; break;
                case "-": result = _lastNumber - current; break;
                case "*": result = _lastNumber * current; break;
                case "/":
                    if (current == 0)
                    {
                        MessageBox.Show("Ділення на нуль!");
                        return;
                    }
                    result = _lastNumber / current;
                    break;
            }

            txtCurrent.Text = result.ToString();
            txtHistory.Text = "";
            _operation = "";
            _isNewNumber = true;
        }

        private void BtnClearAll_Click(object sender, RoutedEventArgs e)
        {
            txtCurrent.Text = "0";
            txtHistory.Text = "";
            _lastNumber = 0;
            _operation = "";
            _isNewNumber = true;
        }

        private void BtnClearEntry_Click(object sender, RoutedEventArgs e)
        {
            txtCurrent.Text = "0";
            _isNewNumber = true;
        }

        private void BtnBackspace_Click(object sender, RoutedEventArgs e)
        {
            if (txtCurrent.Text.Length > 1)
            {
                txtCurrent.Text = txtCurrent.Text.Substring(0, txtCurrent.Text.Length - 1);
            }
            else
            {
                txtCurrent.Text = "0";
                _isNewNumber = true;
            }
        }
    }
}

namespace Lab5Try2
{
    class txtCurrent
    {
        public static string Text { get; internal set; }
    }

    class txtHistory
    {
        public static string Text { get; internal set; }
    }
}