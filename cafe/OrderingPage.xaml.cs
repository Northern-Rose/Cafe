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
    public partial class OrderingPage : Page
    {
        List<Beverages> listOfDrinks;

        List<PriceInfo> listofOrder;

        List<SizesOFCups> listOfDrinkSizes;

        List<CupInfo> listOfDrinkInfo;

        int counter = 0;
        double totalCost = 0;

        public OrderingPage()
        {
            InitializeComponent();


            var TypesOfDrinklines = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_List.csv");

            listOfDrinks = new List<Beverages>();
            listofOrder = new List<PriceInfo>();
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

            var DrinkSizes = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Size_Type.csv");
            listOfDrinkSizes = new List<SizesOFCups>();
            for (int i = 0; i < DrinkSizes.Length; i++)
            {
                var line = DrinkSizes[i].Split(',');
                listOfDrinkSizes.Add(new SizesOFCups
                {
                    sizeID = Int32.Parse(line[0]),
                    CupSize = line[1],
                });
            }

            var drinkInfomation = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_Size_Relationship.csv");
            listOfDrinkInfo = new List<CupInfo>();
            for (int i = 0; i < drinkInfomation.Length; i++)
            {
                var line = drinkInfomation[i].Split(',');
                listOfDrinkInfo.Add(new CupInfo
                {
                    CupInfoID = Int32.Parse(line[0]),
                    CupInfoSize = Int32.Parse(line[1]),
                    CupInfoPrize = double.Parse(line[2], System.Globalization.CultureInfo.InvariantCulture)
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
            var newTeaFilterList = (from d in listOfDrinks where d.DrinkType == "Tea" select d).ToList();

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
            var name = ((Button)sender).Tag;

            Beverages drink = (from d in listOfDrinks where d.Name.Equals(name) select d).First();

            var abc = (from df in listOfDrinkInfo
                       join ds in listOfDrinkSizes on df.CupInfoSize equals ds.sizeID
                       where df.CupInfoID == drink.Id
                       select new PriceInfo
                       {
                           Size = ds.CupSize,
                           Price = df.CupInfoPrize,
                           Display = ds.CupSize + " $" + df.CupInfoPrize,
                           DrinkInfo = drink,
                           FinalDrink = drink.Name + " " + ds.CupSize + " $" + df.CupInfoPrize
                       }
                       ).ToList();

            if (abc.Count > 1)
            {
                ThreeSizePopup.IsOpen = true;
                PopupContent.ItemsSource = abc;
            }
            else
            {
                ThreeSizePopup.IsOpen = false;
                abc[0].location = counter;

                listofOrder.Add(abc[0]);

                counter++;

                totalCost = totalCost + abc[0].Price;

                CostDisplay.Content = "Total Cost: $" + totalCost;

                ListViewOrderedDrinks.ItemsSource = null;
                ListViewOrderedDrinks.ItemsSource = listofOrder;
            }
        }

        private void ThreePopUp_Click(object sender, RoutedEventArgs e)
        {
            var priceInfo = (PriceInfo)(((Button)sender).Tag);

            PriceInfo drink = priceInfo;

            drink.location = counter;

            listofOrder.Add(drink);

            counter++;

            totalCost = totalCost + drink.Price;

            CostDisplay.Content = "Total Cost: $" + totalCost;

            ListViewOrderedDrinks.ItemsSource = null;
            ListViewOrderedDrinks.ItemsSource = listofOrder;

            ThreeSizePopup.IsOpen = false;
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            listofOrder.Clear();
            ListViewOrderedDrinks.ItemsSource = null;

            totalCost = 0;
            CostDisplay.Content = "Total Cost: $" + totalCost;
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
                    PriceInfo itemToRemove = (PriceInfo)item;

                    totalCost = totalCost - itemToRemove.Price;
                    CostDisplay.Content = "Total Cost: $" + totalCost;

                    listofOrder.Remove(itemToRemove);
                }

                ListViewOrderedDrinks.ItemsSource = null;
                ListViewOrderedDrinks.ItemsSource = listofOrder;
            }
        }

        private void ClosePopUp_Click(object sender, RoutedEventArgs e)
        {
            ThreeSizePopup.IsOpen = false;
        }

        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            var datetime = DateTime.Now.ToString("ddMMyyyy-HHmmss");
            FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
                + "\\Homebrew\\Recipets\\" + datetime + ".txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);

            for (int i = 0; i < listofOrder.Count; i++)
            {
                sw.WriteLine(listofOrder[i].FinalDrink);
            }
            sw.Flush();
            sw.Close();
            fs.Close();



            listofOrder.Clear();
            ListViewOrderedDrinks.ItemsSource = null;
            totalCost = 0;
            CostDisplay.Content = "Total Cost: $" + totalCost;
        }

        private void Past_Orders_Click_1(object sender, RoutedEventArgs e)
        {
            ThreeSizePopup.IsOpen = false;

            RecieptPage page = new RecieptPage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }
    }

    public class Beverages
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string URLLink { get; set; }
        public string DrinkType { get; set; }
        public string DrinkCost { get; set; }
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

        public string FinalDrink { get; set; }
        public Beverages DrinkInfo { get; set; }
        public int location { get; set; }
    }
}
