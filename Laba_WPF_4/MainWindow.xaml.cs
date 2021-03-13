using Laba_WPF_4.Classes;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Documents;

namespace Laba_WPF_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            RichTextBoxInput.AppendText("t = 0\ntmax = 0.11\nn = 11\ny1_0 = 1\ny2_0 = 3\ny1' = 2 * (y1 - y1 * y2)\ny2' = -(y2 - y1 * y2)");
        }

        private void ButtonExecute_Click(object sender, RoutedEventArgs e)
        {
            var calculator = new Calculator();
            TextRange textRange = new TextRange(RichTextBoxInput.Document.ContentStart, RichTextBoxInput.Document.ContentEnd);
            string[] rtbLines = textRange.Text.Split('\n');

            List<double[]> result;
            try
            {
                result = calculator.Solve(rtbLines);
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректные данные!", "Предупреждение", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            var step = Math.Round((calculator.Tmax - calculator.T) / calculator.N, 2);
            for (var i = 0; i < calculator.N; i++)
            {
                RichTextBoxResult.AppendText($"time = {(i * step):0.00}\n");

                for (var j = 0; j < result[i].Length; j++)
                {
                    RichTextBoxResult.AppendText($"y[{j}] = {result[i][j]}\n");
                }

                RichTextBoxResult.AppendText("\n");
            }
        }

        private void ButtonClear_Click(object sender, RoutedEventArgs e)
        {
            RichTextBoxResult.Document.Blocks.Clear();
        }
    }
}
