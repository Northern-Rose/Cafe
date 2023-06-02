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
    /// Interaction logic for OrderingPage.xaml
    /// </summary>
    /// 

    public partial class OrderingPage : Page
    {
        List<Beverages> listOfDrinks;

        List<Beverages> listofOrder;
        public OrderingPage()
        {
            InitializeComponent();

            var lines = File.ReadAllLines(@"../../ExcelLists/Drink_List.csv");

            listOfDrinks = new List<Beverages>();
            listofOrder = new List<Beverages>();
            for (int i = 0; i < lines.Length; i++)
            {
                var line = lines[i].Split(',');

                listOfDrinks.Add(new Beverages
                {
                    Name = line[0],
                    URLLink = "/images/" + line[1],
                    DrinkType = line[2]
                });
            }

            ListViewProperty.ItemsSource = listOfDrinks;
        }


        private void Coffee_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newCoffeeFilterList = (from d in listOfDrinks
                                 where d.DrinkType == "Coffee"
                                 select d
                                 ).ToList();

            ListViewProperty.ItemsSource = newCoffeeFilterList;
        }

        private void Tea_drinks_Click(object sender, RoutedEventArgs e)
        {
            var newTeaFilterList = (from d in listOfDrinks
                                where d.DrinkType == "Tea"
                                select d
                                 ).ToList();

            ListViewProperty.ItemsSource = newTeaFilterList;
        }

        private void Soft_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newSoftFilterList = (from d in listOfDrinks
                                    where d.DrinkType == "Soft"
                                    select d
                                 ).ToList();

            ListViewProperty.ItemsSource = newSoftFilterList;
        }

        private void Cold_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newColdFilterList = (from d in listOfDrinks
                                    where d.DrinkType == "Cold"
                                    select d
                                 ).ToList();

            ListViewProperty.ItemsSource = newColdFilterList;
        }

        private void Choco_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newChocoFilterList = (from d in listOfDrinks
                                    where d.DrinkType == "Choco"
                                      select d
                                 ).ToList();

            ListViewProperty.ItemsSource = newChocoFilterList;
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage page = new LoginPage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var name = ((Button) sender).Tag;

            Beverages drink = (from d in listOfDrinks
                         where d.Name.Equals(name)
                         select d
                         ).First();

            listofOrder.Add(drink);
        }
    }

    public class Beverages 
    {
        public string Name { get; set; }
        public string URLLink { get; set; }

        public string DrinkType { get; set; }
    }
}
