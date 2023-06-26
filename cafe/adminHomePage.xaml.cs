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

using cafe;

namespace cafe
{
    /// <summary>
    /// Interaction logic for adminHomePage.xaml
    /// </summary>
    public partial class adminHomePage : Page
    {
        List<Beverages> listOfDrinks;
        List<StaffMember> staffMembers; 

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
            staffMembers = new List<StaffMember>(); 
        }

        private void LogOutButton_Click(object sender, RoutedEventArgs e)
        {
            LoginPage page = new LoginPage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = txtSearch.Text.Trim();

            // Update the visibility of the search placeholder based on the search text
            if (txtSearch.Text != "")
            {
                txtSearchPlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                txtSearchPlaceholder.Visibility = Visibility.Visible;
            }

             }

        private void StaffButton_Click(object sender, RoutedEventArgs e)
        {

            Staff_Details page = new Staff_Details(); 
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page; 
        }

            private string GenerateUniqueCode()
        {
            
            
            string code = "STAFF" + DateTime.Now.ToString("yyyyMMddHHmmss"); // Generate a unique code not sure if we are using numbers or letter or both 
            return code;
        }
        public class StaffMember //public class for staff. 
        {
            public string Name { get; set; }
            public string Position { get; set; }
            public string Code { get; set; }
        } 
    }
 }
  



