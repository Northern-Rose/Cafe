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

        List<SizesOFCups> listOfDrinkSizes;

        List<CupInfo> listOfDrinkInfo;

        int counter = 0;
        public OrderingPage()
        {
            InitializeComponent();

            var TypesOfDrinklines = File.ReadAllLines(@"../../ExcelLists/Drink_List.csv");

            listOfDrinks = new List<Beverages>();
            listofOrder = new List<Beverages>();
            for (int i = 0; i < TypesOfDrinklines.Length; i++)
            {
                var line = TypesOfDrinklines[i].Split(',');

                listOfDrinks.Add(new Beverages
                {
                    Id = Int32.Parse(line[0]),
                    Name = line[1],
                    URLLink = "/images/" + line[2],
                    DrinkType = line[3],
                });
            }

            ListViewProperty.ItemsSource = listOfDrinks;

            var DrinkSizes = File.ReadAllLines(@"../../ExcelLists/Size_Type.csv");
            listOfDrinkSizes = new List<SizesOFCups>();
            for (int i = 0; i < DrinkSizes.Length; i++)
            {
                var line = DrinkSizes[i].Split(',');
                listOfDrinkSizes.Add(new SizesOFCups
                {
                    sizeID = Int32.Parse( line[0]),
                    CupSize = line[1],
                });
            }

            

            var drinkInfomation = File.ReadAllLines(@"../../ExcelLists/Drink_Size_Relationship.csv");
            listOfDrinkInfo = new List<CupInfo>();
            for (int i = 0; i < drinkInfomation.Length; i++)
            {
                var line = drinkInfomation[i].Split(',');
                listOfDrinkInfo.Add(new CupInfo
                {
                    CupInfoID = Int32.Parse( line[0]),
                    CupInfoSize = Int32.Parse( line[1]),
                    CupInfoPrize = Double.Parse( line[2]),
                });
            }

            
        }

        private void Coffee_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newCoffeeFilterList = (from d in listOfDrinks where d.DrinkType == "Coffee" select d).ToList();

            ListViewProperty.ItemsSource = newCoffeeFilterList;
        }

        private void Tea_drinks_Click(object sender, RoutedEventArgs e)
        {
            var newTeaFilterList = (from d in listOfDrinks where d.DrinkType == "Tea"  select d).ToList();

            ListViewProperty.ItemsSource = newTeaFilterList;
        }

        private void Soft_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newSoftFilterList = (from d in listOfDrinks where d.DrinkType == "Soft" select d).ToList();

            ListViewProperty.ItemsSource = newSoftFilterList;
        }

        private void Cold_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newColdFilterList = (from d in listOfDrinks where d.DrinkType == "Cold" select d).ToList();

            ListViewProperty.ItemsSource = newColdFilterList;
        }

        private void Choco_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newChocoFilterList = (from d in listOfDrinks where d.DrinkType == "Choco" select d).ToList();

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

            Beverages drink = (from d in listOfDrinks where d.Name.Equals(name) select d).First();

            var abc = (from df in listOfDrinkInfo
                       join ds in listOfDrinkSizes
                       on df.CupInfoSize equals ds.sizeID
                       where df.CupInfoID == drink.Id
                       select new PriceInfo
                       {
                           Size = ds.CupSize,
                           Price = df.CupInfoPrize,
                           Display = ds.CupSize + " $" + df.CupInfoPrize,
                           DrinkName = drink.Name
                       }
                       ).ToList();

            PopupContent.ItemsSource = abc;
            if (abc.Count == 3)
            {
                ThreeSizePopup.IsOpen = true;
            }
            else if (abc.Count == 2)
            {
                TwoSizePopUp.IsOpen = true;
            }
            else 
            {
                drink.location = counter;

                listofOrder.Add(drink);

                counter++;

                ListViewOrderedDrinks.ItemsSource = null;
                ListViewOrderedDrinks.ItemsSource = listofOrder;
            }

            
        }

        private void ThreePopUp_Click(object sender, RoutedEventArgs e) 
        {
            var priceInfo = (PriceInfo)(((Button)sender).Tag);

            

            //drink.location = counter;

            //listofOrder.Add();

            //counter++;

            //ListViewOrderedDrinks.ItemsSource = null;
            //ListViewOrderedDrinks.ItemsSource = listofOrder;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            listofOrder.Clear();
            ListViewOrderedDrinks.ItemsSource = null;
        }

        private void All_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newAllDrinkFilterList = (from d in listOfDrinks select d).ToList();

            ListViewProperty.ItemsSource = newAllDrinkFilterList;
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e) 
        {
            if (e.Key != Key.Delete) return;

            var abc = ListViewOrderedDrinks.SelectedItems;

            if (abc.Count != 0)
            {
                foreach (var item in abc)
                {
                    Beverages itemToRemove = (Beverages)item;

                    listofOrder.Remove(itemToRemove);
                }

                ListViewOrderedDrinks.ItemsSource = null;
                ListViewOrderedDrinks.ItemsSource = listofOrder;
            }
        }

        private void ClosePopUp_Click(object sender, RoutedEventArgs e)
        {
            ThreeSizePopup.IsOpen = false;
            TwoSizePopUp.IsOpen = false;
        }

    }

    public class Beverages 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URLLink { get; set; }
        public string DrinkType { get; set; }
        public string DrinkCost { get; set; }
        public int location { get; set; }
    }

    public class SizesOFCups 
    {
        public int sizeID { get; set; }
        public string CupSize { get; set; }
    }

    public class CupInfo
    {
        public int CupInfoID { get; set; }
        public int CupInfoSize { get; set; }
        public double CupInfoPrize { get; set; }
    }

    public class PriceInfo
    {
        public string Size { get; set; }
        public double Price { get; set; }
        public string Display { get; set; }

        public string DrinkName { get; set; }
    }
}
