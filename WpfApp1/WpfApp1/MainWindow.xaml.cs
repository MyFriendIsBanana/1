using System;
using System.Windows;
using System.Windows.Controls;

namespace WpfLab4
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitializeTask6();
        }

        // Завдання 1
        private void BtnHello_Click(object sender, RoutedEventArgs e) => lblStatus.Content = "Привіт";
        private void BtnBye_Click(object sender, RoutedEventArgs e) => lblStatus.Content = "До побачення";

        // Завдання 2
        private void BtnHide2_Click(object sender, RoutedEventArgs e) => txtBlock2.Visibility = Visibility.Collapsed;
        private void BtnShow2_Click(object sender, RoutedEventArgs e) => txtBlock2.Visibility = Visibility.Visible;

        // Завдання 3
        private void BtnHide3_Click(object sender, RoutedEventArgs e) => txtInput3.Visibility = Visibility.Collapsed;
        private void BtnShow3_Click(object sender, RoutedEventArgs e) => txtInput3.Visibility = Visibility.Visible;
        private void BtnClear3_Click(object sender, RoutedEventArgs e) => txtInput3.Clear();

        // Завдання 4 — приховуємо кнопки по черзі
        private void BtnGame_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            btn.Visibility = btn.Visibility == Visibility.Visible ? Visibility.Collapsed : Visibility.Visible;

            // Перевірка: чи всі приховані?
            bool allHidden = true;
            foreach (var child in ((Grid)((TabItem)((TabControl)this.Content).Items[3]).Content).Children)
            {
                if (child is StackPanel sp)
                {
                    foreach (var b in sp.Children)
                        if ((b as Button).Visibility == Visibility.Visible)
                            allHidden = false;
                }
            }
            if (allHidden)
                MessageBox.Show("Вітаємо! Ви приховали всі кнопки!", "Гра завершена");
        }

        // Завдання 5
        private void TxtPounds_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (double.TryParse(txtPounds.Text, out double pounds))
            {
                double kg = pounds * 0.45359237;
                lblKgResult.Text = $"{kg:F2} кг";
            }
            else
            {
                lblKgResult.Text = "Невірний формат";
            }
        }

        // Завдання 6 — динамічне створення кнопок, що займають 2/3 ширини
        private void InitializeTask6()
        {
            string[] colors = { "Red", "Green", "Blue", "Orange", "Purple", "Cyan", "Pink", "Yellow", "Gray", "Brown" };
            foreach (var color in colors)
            {
                Button btn = new Button
                {
                    Content = color,
                    Margin = new Thickness(2),
                    Background = new System.Windows.Media.SolidColorBrush((System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString(color)),
                    Foreground = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.White),
                    HorizontalAlignment = HorizontalAlignment.Stretch
                };
                wrapPanel6.Children.Add(btn);
            }

            // Прив'язка ширини WrapPanel до 2/3 вікна
            wrapPanel6.SizeChanged += (s, e) =>
            {
                wrapPanel6.Width = this.ActualWidth * 2.0 / 3.0;
            };
        }
    }
}