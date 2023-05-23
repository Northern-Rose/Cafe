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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace cafe
{
    public partial class MainWindow : Window
    {
        public int count = 0;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            WindowLogin mw = new WindowLogin();
            mw.Show();
            this.Close();
        }

        private void Coffee_Click(object sender, RoutedEventArgs e)
        {
            coffeeImages.Opacity = 100;
            teaImmages.Opacity = 0;
            Cold_Drink_Immages.Opacity = 0;
        }

        private void Tea_drinks_Click(object sender, RoutedEventArgs e)
        {
            coffeeImages.Opacity = 0;
            teaImmages.Opacity = 100;
            Cold_Drink_Immages.Opacity = 0;
        }

        private void Cold_Drinks_Click(object sender, RoutedEventArgs e)
        {
            coffeeImages.Opacity = 0;
            teaImmages.Opacity = 0;
            Cold_Drink_Immages.Opacity = 100;
        }
    }
}
