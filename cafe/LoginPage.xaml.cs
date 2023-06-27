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
    /// Interaction logic for LoginPage.xaml.
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the OrderingPage class
            OrderingPage page = new OrderingPage();
            // Get a reference to the main window of the application
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            // Set the content of the main window to the newly created OrderingPage instance
            window.Content = page;
        }

        private void Button_Click_Admin(object sender, RoutedEventArgs e)
        {
            // Create a new instance of the adminHomePage class
            adminHomePage page = new adminHomePage();
            // Get a reference to the main window of the application
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            // Set the content of the main window to the newly created adminHomePage instance 
            window.Content = page;
        }
    }
}
