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
    /// <summary>
    /// Interaction logic for OrderingPage.xaml
    /// </summary>
    public partial class OrderingPage : Page
    {
        public OrderingPage()
        {
            InitializeComponent();

            List<int> number = new List<int>();
            number.Add(1);
            number.Add(1);
            number.Add(1);
            number.Add(1);
            number.Add(1);
            number.Add(1);
            number.Add(1);
            number.Add(1);
            number.Add(1);
            number.Add(1);
            number.Add(1);
            ListViewProperty.ItemsSource = number;
        }
    }
}
