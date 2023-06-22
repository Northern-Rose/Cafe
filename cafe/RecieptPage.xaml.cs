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
    /// Interaction logic for RecieptPage.xaml
    /// </summary>
    public partial class RecieptPage : Page
    {
        List<ReceiptInfo> listOfReceipts;
        public RecieptPage()
        {
            InitializeComponent();

            listOfReceipts = new List<ReceiptInfo>();

            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\Recipets");
            FileInfo[] files = di.GetFiles("*.txt");
            foreach (var file in files)
            {
                listOfReceipts.Add(new ReceiptInfo
                {
                    ReceiptName = file.Name
                });
            }
            ListViewProperty.ItemsSource = listOfReceipts;
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            OrderingPage page = new OrderingPage();
            WindowLogin window = (WindowLogin)Application.Current.MainWindow;
            window.Content = page;
        }

        private void txtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (txtSearch.Text != "")
            {
                txtSearchPlaceholder.Visibility = Visibility.Hidden;
            }
            else
            {
                txtSearchPlaceholder.Visibility = Visibility.Visible;
            }
        }

        public class ReceiptInfo
        {
            public string ReceiptName { get; set; }
        }
    }
}
