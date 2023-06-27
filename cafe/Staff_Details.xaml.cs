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

            /*StaffMembers = new List<StaffMember>();

            for (int i = 0; i < TypesOfDrinklines.Length; i++)
            {
                var line = TypesOfDrinklines[i].Split(',');

                StaffMembers.Add(new StaffMember
                {
                    ID = Int32.Parse(line[0]),
                    Name = line[1],
                    Code = line[2],
                });
            }*/

            string newUser;

            if(TypesOfDrinklines.Length == 0)
            {
                newUser = string.Format("{0},{1}, {2}", 1, "Robert", "1234");
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
                newUser = string.Format("{0},{1}, {2}", newUserID, "Robert", "1234");
            }

            using (FileStream fs = new FileStream(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Staff_Members.csv", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.WriteLine(newUser);
                }
            }

        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            StaffMember newStaffMember = new StaffMember();
           newStaffMember.Name = NameTextBox.Text;
           newStaffMember.Code = GenerateUniqueCode();

            // Add the staff member to the list
            StaffMembers.Add(newStaffMember);

            // Save the staff member details to the CSV file

            // Clear the input fields
            NameTextBox.Text = string.Empty;


            // Refresh the ListView
            StaffListView.ItemsSource = null;
            StaffListView.ItemsSource = StaffMembers;
        }


        private string GenerateUniqueCode() 
        {
            Random random = new Random();
            string code = random.Next(1000, 10000).ToString();
            return code;
        }
        public class StaffMember 
        {
            public int ID { get; set; }
            public string Name { get; set; } 
            public string Code { get; set; }
        }


    }
}
