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

            listOfReceipts = new List<ReceiptInfo>();  // Create a new list to store receipt information

            DirectoryInfo di = new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\Recipets");    // Get the directory information for the receipts folder
            FileInfo[] files = di.GetFiles("*.txt"); // Get all the files with the .txt extension in the receipts folder
            foreach (var file in files)
            {
                // Iterate through each file
                listOfReceipts.Add(new ReceiptInfo
                {
                    ReceiptName = file.Name.Replace(".txt", "")  // Add a new ReceiptInfo object to the list of receipts, with the receipt name derived from the file name
                });
            }
            ListViewProperty.ItemsSource = listOfReceipts; // Set the ItemsSource property of the ListViewProperty to the list of receipts

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

        private void receiptId_Button_Click (object sender, RoutedEventArgs e)
        {
            receiptInfo.IsOpen = true; // Open the receiptInfo popup

            string fileName = ((Button)sender).Tag.ToString(); // Get the filename from the Tag property of the button

            string[] lines = File.ReadAllLines(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Homebrew\\Recipets\\" + fileName + ".txt");   // Read all lines from the specified file

            receiptInfoTitle.Content = fileName;   // Set the receiptInfoTitle to the filename
            receiptInfoListView.ItemsSource = lines; // Set the ItemsSource of the receiptInfoListView to the lines array
        }

        public class ReceiptInfo
        {
            public string ReceiptName { get; set; }
        }

        
    }
}
