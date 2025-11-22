using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace WpfLab5
{
    public partial class BestOilWindow : Window
    {
        private double _dailyRevenue = 0.0;

        // Товари міні-кафе: назва → ціна
        private readonly Dictionary<string, double> _cafeItems = new()
        {
            { "Кава", 35 },
            { "Чай", 25 },
            { "Бургер", 60 },
            { "Снікерс", 20 },
            { "Мінеральна вода", 15 }
        };

        private List<(CheckBox cb, TextBox qtyBox)> _cafeControls = new();

        public BestOilWindow()
        {
            InitializeComponent();
            InitializeCafe();
            UpdateTotalLabel();
        }

        private void InitializeCafe()
        {
            foreach (var item in _cafeItems)
            {
                var panel = new StackPanel { Orientation = Orientation.Horizontal, Margin = new Thickness(0, 2, 0, 2) };
                var cb = new CheckBox { Content = item.Key, Margin = new Thickness(0, 0, 10, 0) };
                var priceBox = new TextBox
                {
                    Text = $"{item.Value:F2} грн",
                    IsReadOnly = true,
                    Width = 80,
                    Margin = new Thickness(5, 0, 10, 0)
                };
                var qtyBox = new TextBox
                {
                    Text = "0",
                    Width = 50,
                    IsEnabled = false
                };

                cb.Checked += (s, e) => qtyBox.IsEnabled = true;
                cb.Unchecked += (s, e) => { qtyBox.IsEnabled = false; qtyBox.Text = "0"; };

                panel.Children.Add(cb);
                panel.Children.Add(priceBox);
                panel.Children.Add(new TextBlock { Text = "Кількість:", Margin = new Thickness(5, 0, 5, 0) });
                panel.Children.Add(qtyBox);

                stackCafe.Children.Add(panel);
                _cafeControls.Add((cb, qtyBox));
            }
        }

        private void RbMode_Checked(object sender, RoutedEventArgs e)
        {
            UpdateInputMode();
        }

        private void CmbFuel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateTotalLabel();
        }

        private void TxtAmount_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalLabel();
        }

        private void UpdateInputMode()
        {
            if (rbMoney.IsChecked == true)
            {
                // Режим "по сумі": вводимо грн → отримуємо літри
                gbTotal.Header = "До видачі";
                txtAmount.Text = "0";
            }
            else
            {
                gbTotal.Header = "До сплати";
                txtAmount.Text = "0";
            }
        }

        private double GetFuelPrice()
        {
            if (cmbFuel.SelectedItem is ComboBoxItem item)
                return Convert.ToDouble(item.Tag);
            return 0;
        }

        private double CalculateFuelCost()
        {
            if (!double.TryParse(txtAmount.Text, out double input) || input < 0)
                return 0;

            double pricePerLiter = GetFuelPrice();
            if (rbLiters.IsChecked == true)
            {
                // Введено літри → вартість = літри * ціна
                return input * pricePerLiter;
            }
            else
            {
                // Введено суму → вартість = сума (але показуємо літри у підсумку!)
                return input;
            }
        }

        private double CalculateCafeTotal()
        {
            double total = 0;
            int idx = 0;
            foreach (var (cb, qtyBox) in _cafeControls)
            {
                if ((bool)cb.IsChecked)
                {
                    var itemName = cb.Content.ToString();
                    double price = _cafeItems[itemName];
                    if (int.TryParse(qtyBox.Text, out int qty) && qty > 0)
                    {
                        total += qty * price;
                    }
                }
                idx++;
            }
            return total;
        }

        private void UpdateTotalLabel()
        {
            double fuelCost = CalculateFuelCost();
            double cafeCost = CalculateCafeTotal();
            double total = fuelCost + cafeCost;

            if (rbMoney.IsChecked == true)
            {
                // Показуємо літри, а не суму!
                double pricePerLiter = GetFuelPrice();
                double liters = pricePerLiter > 0 ? fuelCost / pricePerLiter : 0;
                lblTotal.Text = $"{liters:F2} л";
            }
            else
            {
                lblTotal.Text = $"{total:F2} грн";
            }
        }

        private void BtnCalculate_Click(object sender, RoutedEventArgs e)
        {
            double fuelCost = CalculateFuelCost();
            double cafeCost = CalculateCafeTotal();

            if (rbMoney.IsChecked == true)
            {
                // У режимі "по сумі" fuelCost — це введена сума
                _dailyRevenue += fuelCost + cafeCost;
            }
            else
            {
                _dailyRevenue += fuelCost + cafeCost;
            }

            MessageBox.Show($"Розрахунок завершено!\nЗагальна сума: {lblTotal.Text}", "BestOil");
            ResetInputs();
        }

        private void ResetInputs()
        {
            txtAmount.Text = "0";
            foreach (var (cb, qtyBox) in _cafeControls)
            {
                cb.IsChecked = false;
                qtyBox.IsEnabled = false;
                qtyBox.Text = "0";
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBox.Show($"Загальна виручка за день: {_dailyRevenue:F2} грн", "BestOil – Звіт");
        }
    }
}