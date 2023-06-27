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

            // Read the lines of the "Drink_List.csv" file located in the specified directory
            var TypesOfDrinklines = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_List.csv");
           
            // Create a new list to hold instances of the Beverages class
            listOfDrinks = new List<Beverages>();
            listofOrder = new List<PriceInfo>();  // Create a new list to hold instances of the PriceInfo class

            // Iterate through each line in the "Drink_List.csv" file
            for (int i = 0; i < TypesOfDrinklines.Length; i++)
            {
                var line = TypesOfDrinklines[i].Split(','); // Split the line by comma to extract individual values

                // Create a new instance of the Beverages class and add it to the listOfDrinks list
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
                listOfDrinkSizes.Add(new SizesOFCups    // Create a new SizesOFCups object and add it to the listOfDrinkSizes 
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
                listOfDrinkInfo.Add(new CupInfo  // Create a new CupInfo object and add it to the listOfDrinkInfo
                {
                    CupInfoID = Int32.Parse(line[0]),
                    CupInfoSize = Int32.Parse(line[1]),
                    CupInfoPrize = double.Parse(line[2], System.Globalization.CultureInfo.InvariantCulture)
                });
            }
        }

        private void Coffee_Drinks_Click(object sender, RoutedEventArgs e)
        {
            // Filter the listOfDrinks to only include drinks of type "Coffee" 
            var newCoffeeFilterList = (from d in listOfDrinks where d.DrinkType == "Coffee" select d).ToList();

            ListViewProperty.ItemsSource = newCoffeeFilterList;  // Set the filtered list as the ItemsSource for the ListViewProperty
        }

        private void Tea_drinks_Click(object sender, RoutedEventArgs e)
        {
            // Filter the listOfDrinks to only include drinks of type "Tea" 
            var newTeaFilterList = (from d in listOfDrinks where d.DrinkType == "Tea" select d).ToList();

            ListViewProperty.ItemsSource = newTeaFilterList;  // Set the filtered list as the ItemsSource for the ListViewProperty
        }

        private void Soft_Drinks_Click(object sender, RoutedEventArgs e)
        {
            // Filter the listOfDrinks to only include drinks of type "Soft" 
            var newSoftFilterList = (from d in listOfDrinks where d.DrinkType == "Soft" select d).ToList();

            ListViewProperty.ItemsSource = newSoftFilterList;  // Set the filtered list as the ItemsSource for the ListViewProperty
        }

        private void Cold_Drinks_Click(object sender, RoutedEventArgs e)
        {
            // Filter the listOfDrinks to only include drinks of type "Cold"  
            var newColdFilterList = (from d in listOfDrinks where d.DrinkType == "Cold" select d).ToList();

            ListViewProperty.ItemsSource = newColdFilterList;  // Set the filtered list as the ItemsSource for the ListViewProperty
        }

        private void Choco_Drinks_Click(object sender, RoutedEventArgs e)
        {
            // Filter the listOfDrinks to only include drinks of type "Choco"  
            var newChocoFilterList = (from d in listOfDrinks where d.DrinkType == "Choco" select d).ToList();

            ListViewProperty.ItemsSource = newChocoFilterList;  // Set the filtered list as the ItemsSource for the ListViewProperty
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage page = new LoginPage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Get the name from the clicked button's Tag property
            var name = ((Button)sender).Tag;

            // Find the corresponding drink object from the listOfDrinks based on the name
            Beverages drink = (from d in listOfDrinks where d.Name.Equals(name) select d).First();

            var abc = (from df in listOfDrinkInfo   // Retrieve information about the drink, including different sizes and prices
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
                ThreeSizePopup.IsOpen = true; // If there are multiple sizes available for the drink, show a popup with options
                PopupContent.ItemsSource = abc;
            }
            else
            {
                ThreeSizePopup.IsOpen = false; // If there is only one size available, add it to the order directly
                abc[0].location = counter;

                listofOrder.Add(abc[0]);  // Add the selected drink to the list of orders

                counter++;

                totalCost = totalCost + abc[0].Price;   // Update the total cost with the price of the selected drink

                CostDisplay.Content = "Total Cost: $" + totalCost;  // Display the updated total cost on the UI

                ListViewOrderedDrinks.ItemsSource = null; // Update the ListView with the list of ordered drinks
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
            listofOrder.Clear(); // Clear the list of orders
            ListViewOrderedDrinks.ItemsSource = null; // Clear the items source of the ListView to remove all previously displayed orders

            totalCost = 0; // Reset the total cost to zero
            CostDisplay.Content = "Total Cost: $" + totalCost; // Update the UI to display the updated total cost as zero
        }

        private void All_Drinks_Click(object sender, RoutedEventArgs e)
        {
            // Create a new list by selecting all drinks from the listOfDrinks
            var newAllDrinkFilterList = (from d in listOfDrinks select d).ToList();

            ListViewProperty.ItemsSource = newAllDrinkFilterList;    // Set the ItemsSource property of the ListViewProperty to the new list of drinks
        }

        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Delete) return; // Check if the pressed key is not the Delete key

            var abc = ListViewOrderedDrinks.SelectedItems; // Get the selected items from the ListView

            if (abc.Count != 0) // Check if there are selected items
            {
                foreach (var item in abc)
                {
                    PriceInfo itemToRemove = (PriceInfo)item; // Convert the selected item to the PriceInfo type

                    totalCost = totalCost - itemToRemove.Price;   // Subtract the price of the item to be removed from the total cost
                    CostDisplay.Content = "Total Cost: $" + totalCost;  // Update the UI to display the updated total cost

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
            var datetime = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
            FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)  // Create a file stream to write to the file
                + "\\Homebrew\\Recipets\\" + datetime + ".txt", FileMode.Append, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs);  // Create a StreamWriter to write text to the file stream

            for (int i = 0; i < listofOrder.Count; i++)  // Write each item in the list of orders to the file
            {
                sw.WriteLine(listofOrder[i].FinalDrink);
            }
            sw.Flush(); // Flush the stream to ensure all data is written
            sw.Close(); // Close the StreamWriter and FileStream
            fs.Close();



            listofOrder.Clear();  // Clear the list of orders
            ListViewOrderedDrinks.ItemsSource = null; // Clear the ItemsSource of the ListView to remove all previously displayed orders
            totalCost = 0;  // Reset the total cost to zero
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
