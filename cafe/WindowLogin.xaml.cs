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
            LoginPage page = new LoginPage();
            currentDisplayPage = page;

            this.DataContext = this;
        }

        //private void Button_Click(object sender, RoutedEventArgs e)
        //{
        //    MainWindow mw = new MainWindow();
        //    mw.Show();
        //    this.Close();
        //}

        //private void Button_Click_1(object sender, RoutedEventArgs e)
        //{
        //    SampleSaleWindow mw = new SampleSaleWindow();
        //    mw.Show();
        //    this.Close();
        //}

        public Page currentDisplayPage { set; get; }
    }
}
