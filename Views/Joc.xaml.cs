using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MVVMPairs.Services;

namespace MVVMPairs.Views
{
    /// <summary>
    /// Interaction logic for Joc.xaml
    /// </summary>
    public partial class Joc : Window
    {
        public Joc()
        {
            InitializeComponent();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Back_click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Joc window = new Joc();
            window.Show();
            this.Close();
        }

        private void Saritura_Click(object sender, RoutedEventArgs e)
        {
            Saritura.IsEnabled = false;
        }

    }
}
