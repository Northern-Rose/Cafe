﻿using System;
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
    /// Interaction logic for Staff_Details.xaml
    /// </summary>


    public partial class Staff_Details : Page
    {

        // Define a private field to store the staff members
        private List<StaffMember> StaffMembers;

        // Define a public property to access the staff members

        public Staff_Details()
        {
            InitializeComponent();
            // Initialize the staff members list
            StaffMembers = new List<StaffMember>(); // Initialize the staff members list 

            var TypesOfDrinklines = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Staff_Members.csv");

            StaffMembers = new List<StaffMember>();

            for (int i = 0; i < TypesOfDrinklines.Length; i++)
            {
                var line = TypesOfDrinklines[i].Split(',');

                StaffMembers.Add(new StaffMember
                {
                    ID = Int32.Parse(line[0]),
                    Name = line[1],
                    Code = line[2],
                });
            }
            ListViewProperty.ItemsSource = StaffMembers;
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

        private string GenerateUniqueCode()
        {
            Random random = new Random();
            string code = random.Next(1000, 10000).ToString();
            return code;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            LoginPage page = new LoginPage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            adminHomePage page = new adminHomePage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        int random_numberOne;
        int random_numberTwo;
        int random_numberThree;
        int random_numberFour;

        private void AddStaff_Click_1(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < 10; i++)
            {
                Random random = new Random();
                random_numberOne = random.Next(1, 10);
                Console.WriteLine(random_numberOne);
            }
            //random_numberOne = new Random().Next(1, 10);
            //random_numberTwo = new Random().Next(1, 10);
            //random_numberThree = new Random().Next(1, 10);
            //random_numberFour = new Random().Next(1, 10);
            //int newStaffCodeOne = int.Parse(random_numberOne.ToString() + random_numberTwo.ToString());
            //int newStaffCodeTwo = int.Parse(random_numberThree.ToString() + random_numberFour.ToString());
            //int newStaffCodeFinal = int.Parse(newStaffCodeOne.ToString() + newStaffCodeTwo.ToString());

            //Console.WriteLine(newStaffCodeFinal);

            string newUser;
            var TypesOfDrinklines = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Staff_Members.csv");

            if (txtSearch.Text.Trim() != "")
            {
                if (TypesOfDrinklines.Length == 0)
                {
                    newUser = string.Format("{0},{1}, {2}", 1, txtSearch.Text.Trim(), "1234");
                }
                else
                {
                    StaffMembers = new List<StaffMember>();

                    for (int i = 0; i < TypesOfDrinklines.Length; i++)
                    {
                        var line = TypesOfDrinklines[i].Split(',');

                        StaffMembers.Add(new StaffMember
                        {
                            ID = Int32.Parse(line[0]),
                            Name = line[1],
                            Code = line[2],
                        });
                    }

                    int newUserID = StaffMembers.Select(x => x.ID).Max() + 1;
                    newUser = string.Format("{0},{1}, {2}", newUserID, txtSearch.Text.Trim(), "1234");
                }

                using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Staff_Members.csv", FileMode.Append, FileAccess.Write))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.WriteLine(newUser);
                    }
                }
            }
            txtSearch.Text = "";
            ListViewProperty.ItemsSource = StaffMembers;
        }

        public class StaffMember
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public string Code { get; set; }
        }

        private void OrdersButton_Click(object sender, RoutedEventArgs e)
        {
            RecieptPage page = new RecieptPage("Staff_Details");
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }
    }
}
