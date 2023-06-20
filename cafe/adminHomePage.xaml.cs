using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for adminHomePage.xaml
    /// </summary>
    public partial class adminHomePage : Page
    {
        List<Beverages> listOfDrinks;
        public adminHomePage()
        {
            InitializeComponent();

            var TypesOfDrinklines = File.ReadAllLines(@"../../ExcelLists/Drink_List.csv");

            listOfDrinks = new List<Beverages>();
            for (int i = 0; i < TypesOfDrinklines.Length; i++)
            {
                var line = TypesOfDrinklines[i].Split(',');

                listOfDrinks.Add(new Beverages
                {
                    Id = Int32.Parse(line[0]),
                    Name = line[1],
                    URLLink = "/CafeImages/" + line[2],
                    DrinkType = line[3],
                });
            }
            ListViewProperty.ItemsSource = listOfDrinks;
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage page = new LoginPage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                txtSearchPlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                txtSearchPlaceholder.Visibility = Visibility.Visible;
            }
        }
    }
}
