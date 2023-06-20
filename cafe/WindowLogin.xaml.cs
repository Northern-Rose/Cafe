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

            LoginPage page = new LoginPage();

            currentDisplayPage = page;

            this.DataContext = this;
        }

        public Page currentDisplayPage { set; get; }
    }
}
