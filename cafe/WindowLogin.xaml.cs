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
using System.Windows.Shapes;

namespace cafe
{
    /// <summary>
    /// Interaction logic for WindowLogin.xaml
    /// </summary>
    public partial class WindowLogin : Window
    {
        public WindowLogin()
        {
            InitializeComponent();

            string path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew";


            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string Recieptpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\Recipets";

            if (!Directory.Exists(Recieptpath))
            {
                Directory.CreateDirectory(Recieptpath);
            }

            string CSVpath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files";

            if (!Directory.Exists(CSVpath))
            {
                Directory.CreateDirectory(CSVpath);
            }

            

            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_List.csv"))
            {
                File.Copy(System.IO.Path.GetFullPath(@"..\..\ExcelLists\Drink_List.csv"), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_List.csv");

            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_Size_Relationship.csv"))
            {
                File.Copy(System.IO.Path.GetFullPath(@"..\..\ExcelLists\Drink_Size_Relationship.csv"), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Drink_Size_Relationship.csv");

            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Size_Type.csv"))
            {
                File.Copy(System.IO.Path.GetFullPath(@"..\..\ExcelLists\Size_Type.csv"), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Size_Type.csv");

            }
            if (!File.Exists(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Staff_Members.csv"))
            {
                File.Copy(System.IO.Path.GetFullPath(@"..\..\ExcelLists\Staff_Members.csv"), Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\CSV_Files\\Staff_Members.csv");

            }

            LoginPage page = new LoginPage();

            currentDisplayPage = page;

            this.DataContext = this;
        }

        public Page currentDisplayPage { set; get; }
    }
}
