using System;
using System.Collections.Generic;
using System.Data.OleDb;
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
    /// Interaction logic for bsTransactions.xaml
    /// </summary>
    public partial class bsTransactions : Window
    {
        string customerID = "";
        string customerName = "";
        string isbn = "";
        string title = "";
        string pricePerUnit = "";
        string totalPrice = "";
        string transactionID = "";
        string quatity = "";
        string dateTR = "";
        

        string strConn = Properties.Resources.connection;

        void setSAVEdefault()
        {
            var nv = new BrushConverter();
            btnTSsave.Background = (Brush)nv.ConvertFrom("#FFDDDDDD");
        }


        void RefreshGrid(DataGrid dataGrid)
        {
            string strCon = Properties.Resources.connection;
            OleDbConnection conn = new OleDbConnection(strCon);
            conn.Open();

            OleDbCommand command = new OleDbCommand("SELECT * FROM transactions ORDER BY transactionID desc", conn);
            dataGrid.ItemsSource = command.ExecuteReader();
        }

        void nav(Rectangle reg)
        {
            var nv = new BrushConverter();
            reg.Fill = (Brush)nv.ConvertFrom("#FFEE3B54");
        }
        void unnav(Rectangle reg)
        {
            var nv = new BrushConverter();
            reg.Fill = (Brush)nv.ConvertFrom("#FF494949");
        }

        void ClearValue()
        {
            txtShowCusID.Text = "";
            txtShowCustomer.Text = "";
            txtShowBookID.Text = "";
            txtShowBook.Text = "";
            txtPricePerUnit.Text = "";
            txtSum.Text = "";

            txtQuatity.Text = "";
            txtDate.Text = "";

            cbbSelectCustomer.SelectedIndex = -1;
            cbbSelectBook.SelectedIndex = -1;


        }

        public bsTransactions()
        {
            InitializeComponent();
            fill_cus_combo();
            fill_book_combo();
            nav(regNav1);

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            bsMain bsMain = new bsMain();
            this.Visibility = Visibility.Hidden;
            bsMain.Show();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            RefreshGrid(dtgMain);
        }
        private void txtShowCusID_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerID = txtShowCusID.Text.Trim();
        }
        private void txtShowCustomer_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerName = txtShowCustomer.Text.Trim();
        }
        private void txtShowBookID_TextChanged(object sender, TextChangedEventArgs e)
        {
            isbn = txtShowBookID.Text.Trim();
        }
        private void txtShowBook_TextChanged(object sender, TextChangedEventArgs e)
        {
            title = txtShowBook.Text.Trim();
        }

        private void txtPricePerUnit_TextChanged(object sender, TextChangedEventArgs e)
        {
            pricePerUnit = txtPricePerUnit.Text.Trim();
        }

        private void txtSum_TextChanged(object sender, TextChangedEventArgs e)
        {
            totalPrice = txtSum.Text.Trim();
        }

        private void txtTRid_TextChanged(object sender, TextChangedEventArgs e)
        {
            transactionID = txtTRid.Text.Trim();
        }
        private void txtDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            unnav(regNav4);
            btnTSsave.Background = Brushes.LightGreen;
            dateTR = txtDate.Text.Trim();
        }
        private void txtQuatity_TextChanged(object sender, TextChangedEventArgs e)
        {
            quatity = txtQuatity.Text.Trim();
        }

        void fill_cus_combo()
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strComm = "SELECT * FROM customers ORDER BY customerID desc;";
                OleDbCommand createComm = new OleDbCommand(strComm, conn);

                OleDbDataReader dataR = createComm.ExecuteReader();
                while (dataR.Read())
                {
                    string n = dataR.GetString(1);
                    cbbSelectCustomer.Items.Add(n);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbSelectCustomer_DropDownClosed(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string Query = "SELECT * FROM customers WHERE customerName = '" + cbbSelectCustomer.Text + "' ";
                OleDbCommand createComm = new OleDbCommand(Query, conn);

                OleDbDataReader dataR = createComm.ExecuteReader();
                while (dataR.Read())
                {
                    string customerID = dataR.GetInt32(0).ToString();
                    string customerName = dataR.GetString(1);
                    txtShowCusID.Text = customerID;
                    txtShowCustomer.Text = customerName;
                }

                if (cbbSelectCustomer.SelectedIndex < 0)
                {
                    nav(regNav1);
                }
                else if (cbbSelectCustomer.SelectedIndex > -1)
                {
                    nav(regNav2);
                    unnav(regNav1);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void fill_book_combo()
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string strComm = "SELECT * FROM books ORDER BY ISBN desc;";
                OleDbCommand createComm = new OleDbCommand(strComm, conn);

                OleDbDataReader dataR = createComm.ExecuteReader();
                while (dataR.Read())
                {
                    string n = dataR.GetString(1);
                    cbbSelectBook.Items.Add(n);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbbSelectBook_DropDownClosed(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string Query = "SELECT * FROM books WHERE title = '" + cbbSelectBook.Text + "' ";
                OleDbCommand createComm = new OleDbCommand(Query, conn);

                OleDbDataReader dataR = createComm.ExecuteReader();
                while (dataR.Read())
                {
                    string isbn = dataR.GetInt32(0).ToString();
                    string title = dataR.GetString(1);
                    string pricePerUnit = dataR.GetDecimal(3).ToString(); // ตัวเลขในวงเล็บหมายถึงลำดับของตำแหน่งคอลัมน์หรือป่าว ถ้านับ 0 - 3 pricePerUnit จะอยู่ตำแหน่งที่ 3 พอดี 

                    txtShowBookID.Text = isbn;
                    txtShowBook.Text = title;
                    txtPricePerUnit.Text = pricePerUnit;
                }
                if (cbbSelectBook.SelectedIndex < 0)
                {
                    nav(regNav2);
                }
                else if(cbbSelectBook.SelectedIndex > -1)
                {
                    nav(regNav3);
                    unnav(regNav2);
                }
                txtQuatity.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void txtQuatity_LostFocus(object sender, RoutedEventArgs e)
        {
            int qOK;
            string q = txtQuatity.Text;
            int.TryParse(q, out qOK);

            int ppu;
            string p = txtPricePerUnit.Text;
            int.TryParse(p, out ppu);

            int s;
            s = qOK*ppu;
            txtSum.Text = s.ToString();

            if (txtQuatity.Text == "")
            {
                nav(regNav3);
            }
            else if(txtQuatity.Text != "" && cbbSelectCustomer.SelectedIndex > -1 && cbbSelectBook.SelectedIndex > -1)
            {
                nav(regNav4);
                unnav(regNav3);
            }
            
            

        }



        private void btnTScancel_Click(object sender, RoutedEventArgs e)
        {
            ClearValue();
            nav(regNav1);
            unnav(regNav4);
            setSAVEdefault();
        }

        private void btnTSsave_Click(object sender, RoutedEventArgs e)
        {
            if (cbbSelectCustomer.SelectedIndex > 0 && cbbSelectBook.SelectedIndex > 0 && txtQuatity.Text != "" && txtDate.Text != "")
            {
                OleDbConnection conn = new OleDbConnection(strConn);
                try
                {
                    MessageBoxResult result = MessageBox.Show("Comfirm to Save ?", "New Record", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        conn.Open();

                        string strComm = "INSERT INTO transactions(ISBN, customerID, quatity, pricePerUnit, totalPrice, orderDate) VALUES ('" + isbn + "','" + customerID + "','" + quatity + "','" + pricePerUnit + "','" + totalPrice + "','" + dateTR + "');";
                        OleDbCommand createComm = new OleDbCommand(strComm, conn);
                        createComm.ExecuteNonQuery();

                        conn.Close();
                        ClearValue();
                        RefreshGrid(dtgMain);

                        var nv = new BrushConverter();
                        btnTSsave.Background = (Brush)nv.ConvertFrom("#FFDDDDDD");

                        MessageBox.Show("Saved!", "Your Welcome", MessageBoxButton.OK);
                    }
                    else
                    {
                        ClearValue();
                    }

                }

                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Put Value Please.","Incomplete Infomation.",MessageBoxButton.OK,MessageBoxImage.Stop);
            }
        }

        private void btnCheckOrder_Click(object sender, RoutedEventArgs e)
        {
            string strCon = Properties.Resources.connection;
            OleDbConnection conn = new OleDbConnection(strCon);
            conn.Open();

            OleDbCommand command = new OleDbCommand("SELECT transactions.transactionID, transactions.ISBN, books.title, customers.customerName, transactions.quatity, transactions.pricePerUnit, transactions.totalPrice, transactions.orderDate FROM (transactions INNER JOIN customers ON transactions.customerID = customers.customerID)INNER JOIN books ON transactions.ISBN = books.ISBN ORDER BY orderDate desc, customerName asc;", conn);
            dtgMain.ItemsSource = command.ExecuteReader();
        }

        private void txtQuatity_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                request.Wrapped = true;
                ((TextBox)sender).MoveFocus(request);
            }
        }

        private void txtDate_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                request.Wrapped = true;
                ((TextBox)sender).MoveFocus(request);
            }
        }
    }       
}
