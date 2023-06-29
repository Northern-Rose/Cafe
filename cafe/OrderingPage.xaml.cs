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
        /*
         A couple of different lists which holds
        information relating to the specifcs of lists
        the listOfDrinks hold all the drink names and images as an example
        the listOfDrinkInfo holds the info for sizes of the drinks and the prices for the sizes
        ListOfDrinkSize holds the id for the different sizes
        And ListOfOrder displays the drinks and their sizes and price in the purchase area
         */
        List<Beverages> listOfDrinks;

        List<PriceInfo> listofOrder;

        List<SizesOFCups> listOfDrinkSizes;

        List<CupInfo> listOfDrinkInfo;

        /*
        counter is used further down in the code to keep track of what order
        all the drinks have been into so you can easily remove them if needed
        */
        int counter = 0;


        // Total Cost is used further down and holds the total costs of all the drinks in the order

        double totalCost = 0;

        /*
         Declaring the string codeInput here so it can be used in several areas and not limited
         to 1 place as it's called upon in various functions
         */
        string codeInput = "";

        public OrderingPage()
        {
            InitializeComponent();

            // Reads all the lines of the CSV file "Drink_List.csv" located in the specified directory
            var TypesOfDrinklines = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_List.csv");
            // Create a new list to hold instances of the Beverages class
            listOfDrinks = new List<Beverages>();
            // Create a new list to hold instances of the PriceInfo class
            listofOrder = new List<PriceInfo>();  

            // Iterates through each line in the "Drink_List.csv" file
            for (int i = 0; i < TypesOfDrinklines.Length; i++)
            {
                // Split the line by comma to extract individual values
                var line = TypesOfDrinklines[i].Split(','); 

                // Creates a new instance of the Beverages class and adds it to the listOfDrinks list storing all infomation from the CSV File
                listOfDrinks.Add(new Beverages
                {
                    Id = Int32.Parse(line[0]),
                    Name = line[1],
                    URLLink = "/CafeImages/" + line[2],
                    DrinkType = line[3],
                });
            }
            //Sends info from the list to the Listview to everything is displayed properly
            ListViewProperty.ItemsSource = listOfDrinks;

            // Reads all the lines of the CSV file "Size_Type.csv" located in the specified directory
            var DrinkSizes = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Size_Type.csv");
            // Create a new list to hold instances of the SizesOFCups class
            listOfDrinkSizes = new List<SizesOFCups>();

            // Iterates through each line in the "Size_Type.csv" file
            for (int i = 0; i < DrinkSizes.Length; i++)
            {
                // Split the line by comma to extract individual values
                var line = DrinkSizes[i].Split(',');
                // Creates a new SizesOFCups object and adds it to the listOfDrinkSizes list storing all infomation from the CSV File
                listOfDrinkSizes.Add(new SizesOFCups
                {
                    sizeID = Int32.Parse(line[0]),
                    CupSize = line[1],
                });
            }

            // Reads all the lines of the CSV file "Drink_Size_Relationship.csv" located in the specified directory
            var drinkInfomation = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_Size_Relationship.csv");
            // Create a new list to hold instances of the CupInfo class
            listOfDrinkInfo = new List<CupInfo>();

            // Iterates through each line in the "Drink_Size_Relationship.csv" file
            for (int i = 0; i < drinkInfomation.Length; i++)
            {
                // Split the line by comma to extract individual values
                var line = drinkInfomation[i].Split(',');
                // Create a new CupInfo object and add it to the listOfDrinkInfo list storing all infomation from the CSV File
                listOfDrinkInfo.Add(new CupInfo  
                {
                    CupInfoID = Int32.Parse(line[0]),
                    CupInfoSize = Int32.Parse(line[1]),
                    CupInfoPrize = double.Parse(line[2], System.Globalization.CultureInfo.InvariantCulture)
                });
            }
        }

        // Filter the listOfDrinks to only include drinks of type "Coffee" 
        private void Coffee_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newCoffeeFilterList = (from d in listOfDrinks where d.DrinkType == "Coffee" select d).ToList();

            // Sets the filtered list as the ItemsSource for the ListView
            ListViewProperty.ItemsSource = newCoffeeFilterList; 
        }

        // Filter the listOfDrinks to only include drinks of type "Tea" 
        private void Tea_drinks_Click(object sender, RoutedEventArgs e)
        {
            var newTeaFilterList = (from d in listOfDrinks where d.DrinkType == "Tea" select d).ToList();

            // Sets the filtered list as the ItemsSource for the ListView
            ListViewProperty.ItemsSource = newTeaFilterList;  
        }

        // Filter the listOfDrinks to only include drinks of type "Soft" 
        private void Soft_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newSoftFilterList = (from d in listOfDrinks where d.DrinkType == "Soft" select d).ToList();

            // Sets the filtered list as the ItemsSource for the ListView
            ListViewProperty.ItemsSource = newSoftFilterList; 
        }

        // Filter the listOfDrinks to only include drinks of type "Cold"  
        private void Cold_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newColdFilterList = (from d in listOfDrinks where d.DrinkType == "Cold" select d).ToList();

            // Sets the filtered list as the ItemsSource for the ListView
            ListViewProperty.ItemsSource = newColdFilterList;  
        }

        // Filter the listOfDrinks to only include drinks of type "Choco"  
        private void Choco_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newChocoFilterList = (from d in listOfDrinks where d.DrinkType == "Choco" select d).ToList();

            // Sets the filtered list as the ItemsSource for the ListView
            ListViewProperty.ItemsSource = newChocoFilterList;  
        }

        // Creates a new list by selecting all drinks from the listOfDrinks and displays them all
        private void All_Drinks_Click(object sender, RoutedEventArgs e)
        {
            var newAllDrinkFilterList = (from d in listOfDrinks select d).ToList();

            // Set the ItemsSource property of the ListViewProperty to the new list of drinks
            ListViewProperty.ItemsSource = newAllDrinkFilterList;
        }

        //This button send the user back to the login window
        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage page = new LoginPage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        //This button shows the popups for the corresponding drinks or adds the drink directly to the order view when clicked
        private void AddDrinkToOrder_Click(object sender, RoutedEventArgs e)
        {
            // Gets the name from the clicked button's Tag property
            var name = ((Button)sender).Tag;

            // Finds the corresponding drink object from the listOfDrinks based on the name
            Beverages drink = (from d in listOfDrinks where d.Name.Equals(name) select d).First();

            // Retrieves information about the drink, including different sizes and prices
            // Puts the final display information into one variable for easy use
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

            // This if statement checks if there are several sizes for a drink or only 1

            if (abc.Count > 1)
            {
                //If there are several sizes it opens the popup for size selection
                ThreeSizePopup.IsOpen = true; 
                PopupContent.ItemsSource = abc;
            }
            else
            {
                /*
                 If there is only 1 size it instantly adds it to the ordered drinks view
                 adds the cost to the toal costs of the drink
                */
                ThreeSizePopup.IsOpen = false;
                // Stores the location of the drink
                abc[0].location = counter;

                // Adds the selected drink to the list of orders
                listofOrder.Add(abc[0]);  

                //Counter goes up to store the location for this drink
                counter++;

                // Updates the total cost with the price of the selected drink
                totalCost = totalCost + abc[0].Price;

                // Display the updated total cost on the UI
                CostDisplay.Content = "Total Cost: $" + totalCost;

                // Updates the ListView with the list of ordered drinks
                ListViewOrderedDrinks.ItemsSource = null;
                ListViewOrderedDrinks.ItemsSource = listofOrder;
            }
        }

        // The popup for the different sizes
        private void ThreePopUp_Click(object sender, RoutedEventArgs e)
        {
            // Get's the prices for the different sizes and displays them
            var priceInfo = (PriceInfo)(((Button)sender).Tag);

            PriceInfo drink = priceInfo;

            // Stores the location of the drink
            drink.location = counter;

            // Adds the selected drink to the list of orders
            listofOrder.Add(drink);

            // Counter goes up to store the location for this drink
            counter++;

            // Updates the total cost with the price of the selected drink
            totalCost = totalCost + drink.Price;

            // Display the updated total cost on the UI
            CostDisplay.Content = "Total Cost: $" + totalCost;

            // Updates the ListView with the list of ordered drinks
            ListViewOrderedDrinks.ItemsSource = null;
            ListViewOrderedDrinks.ItemsSource = listofOrder;

            //Closes the popup
            ThreeSizePopup.IsOpen = false;
        }

        // This funcion clears the entire list of orders and resets the cost to 0
        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            listofOrder.Clear(); 
            ListViewOrderedDrinks.ItemsSource = null; 

            totalCost = 0; // 
            CostDisplay.Content = "Total Cost: $" + totalCost; 
        }

        //This function removes one or several selected items from the list of orders
        private void ListView_KeyDown(object sender, KeyEventArgs e)
        {
            // Checks if the pressed key is not the Delete key
            if (e.Key != Key.Delete) return;

            // Gets the selected items from the ListView
            var abc = ListViewOrderedDrinks.SelectedItems;

            // Checks if there are selected items
            if (abc.Count != 0) 
            {
                foreach (var item in abc)
                // Convert the selected item/items to the PriceInfo type
                {
                    PriceInfo itemToRemove = (PriceInfo)item;

                    // Subtracts the price of the item to be removed from the total cost
                    totalCost = totalCost - itemToRemove.Price;

                    // Updates the UI to display the updated total cost
                    CostDisplay.Content = "Total Cost: $" + totalCost;  

                    //Removes the actual item/items from the orders list
                    listofOrder.Remove(itemToRemove);
                }
                // Updates the display of the list of orders
                ListViewOrderedDrinks.ItemsSource = null;
                ListViewOrderedDrinks.ItemsSource = listofOrder;
            }
        }

        // Closes the pupup for the different size selection
        private void ClosePopUp_Click(object sender, RoutedEventArgs e)
        {
            ThreeSizePopup.IsOpen = false;
        }

        // Opens the number pad for staff users to input their staff code
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            StaffCodeInput.IsOpen = true;
        }

        // Brings you to the page for previous orders
        private void Past_Orders_Click_1(object sender, RoutedEventArgs e)
        {
            ThreeSizePopup.IsOpen = false;

            RecieptPage page = new RecieptPage("OrderingPage");
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        // Closes the staff number pad popup
        private void CloseStaffPop_Click(object sender, RoutedEventArgs e)
        {
            StaffCodeInput.IsOpen = false;
        }

        // Get's the input from the numpads and displays the code
        private void StaffCalcualtor_Click(object sender, RoutedEventArgs e)
        {
            codeInput = codeInput + ((Button)sender).Tag.ToString();
            CodeDisplay.Text = codeInput;
        }

        private void FinalizeOrder_Click(object sender, RoutedEventArgs e)
        {
            // If the list of orders is not empty it creates a new order/reciept and saves it
            if (listofOrder.Count != 0)
            {
                //The name of the reciept is the current date, time and the staff users code
                var datetime = DateTime.Now.ToString("dd-MM-yyyy HH.mm.ss");
                // Creates a file stream to write to the file
                FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)  
                    + "\\Homebrew\\Recipets\\" + datetime + " " + codeInput + ".txt", FileMode.Append, FileAccess.Write);
                // Creates a StreamWriter to write text to the file stream
                StreamWriter sw = new StreamWriter(fs);

                // Writes each item in the list of orders to the file
                for (int i = 0; i < listofOrder.Count; i++) 
                {
                    sw.WriteLine(listofOrder[i].FinalDrink);
                }
                // Flushes the stream to ensure all data is written
                sw.Flush();
                // Closes the StreamWriter and FileStream
                sw.Close(); 
                fs.Close();

                // Clears the list of orders
                listofOrder.Clear();
                // Clears the ItemsSource of the ListView to remove all previously displayed orders
                ListViewOrderedDrinks.ItemsSource = null;
                // Resets the total cost to zero
                totalCost = 0;  
                CostDisplay.Content = "Total Cost: $" + totalCost;

                codeInput = "";
                CodeDisplay.Text = codeInput;
                StaffCodeInput.IsOpen = false;
            }
        }

        private void BackspaceNumber_Click(object sender, RoutedEventArgs e)
        {
            int inter = codeInput.Length - 1; // Subtract 1 to get the last character index

            if (inter >= 0 && inter < codeInput.Length)
            {
                codeInput = codeInput.Remove(inter, 1);
                CodeDisplay.Text = codeInput;
            }
        }
    }
    /*
    Here are all the different classes which helps store and transfer 
    information between the CSV files and the different variables
    */
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

    public class StaffMember
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
