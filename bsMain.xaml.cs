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

namespace wpfBookStores
{
    /// <summary>
    /// Interaction logic for bsMain.xaml
    /// </summary>
    public partial class bsMain : Window
    {
        public bsMain()
        {
            InitializeComponent();
            
        }
        private void btnLogOut_Click(object sender, RoutedEventArgs e)
        {
            bsLogin login = new bsLogin();
            this.Visibility = Visibility.Hidden;
            login.Show();
        }
        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Maximized;
        }

        private void btnBooksManage_Click(object sender, RoutedEventArgs e)
        {
            bsBookManage bsBM = new bsBookManage();
            this.Visibility = Visibility.Hidden;
            bsBM.Show();
        }


        private void btnCustomersManage_Click(object sender, RoutedEventArgs e)
        {
            bsCustomer bsCM = new bsCustomer();
            this.Visibility = Visibility.Hidden;
            bsCM.Show();
        }

        private void btnTransaction_Click(object sender, RoutedEventArgs e)
        {
            bsTransactions bsTR = new bsTransactions();
            this.Visibility = Visibility.Hidden;
            bsTR.Show();
        }
    }
}
