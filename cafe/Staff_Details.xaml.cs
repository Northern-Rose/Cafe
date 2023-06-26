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
    /// Interaction logic for Staff_Details.xaml
    /// </summary>
    public partial class Staff_Details : Page
    {
        // Define a private field to store the staff members
        private List<StaffMember> StaffMembers; 

       // Define a public property to access the staff members
        public List <StaffMember> staffMembers  
        {
            //Return the value of the private field 
            get { return StaffMembers; }

            //Empty setter as we don't need to modify the property directly
            set { } 


        }
        public Staff_Details()
        {
            InitializeComponent();
            // Initialize the staff members list
            StaffMembers = new List<StaffMember>(); // Initialize the staff members list 


            LoadStaffMembers(); // Load existing staff members from the CSV file

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            StaffMember newStaffMember = new StaffMember();
           newStaffMember.Name = NameTextBox.Text;
           newStaffMember.Code = GenerateUniqueCode();

            // Add the staff member to the list
            StaffMembers.Add(newStaffMember);

            // Save the staff member details to the CSV file
            SaveStaffMembers();

            // Clear the input fields
            NameTextBox.Text = string.Empty;


            // Refresh the ListView
            StaffListView.ItemsSource = null;
            StaffListView.ItemsSource = StaffMembers;
        }

        private void LoadStaffMembers()
        {
            //load staff members to the csv file
        }

        private void SaveStaffMembers()
        {
            //save staff members to the csv file
        }




        private string GenerateUniqueCode() 
        {
            Random random = new Random();
            string code = random.Next(1000, 10000).ToString();
            return code;
        }
        public class StaffMember 
        {
            public string Name { get; set; } 
            public string Code { get; set; }
        }


    }
}
