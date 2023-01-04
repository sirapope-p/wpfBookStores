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
    /// Interaction logic for bsCustomer.xaml
    /// </summary>
    public partial class bsCustomer : Window
    {
        string customerID = "";
        string customerName = "";
        string address = "";
        string email = "";
        string strConn = Properties.Resources.connection;

        void fill_cbb()
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
                    cbbUCid.Items.Add(n);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void fill_cbb_delete()
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
                    cbbDCid.Items.Add(n);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void clear_cbb()
        {
            cbbUCid.Items.Clear();
            cbbDCid.Items.Clear();
        }
        void SetColorButton(Button button)
        {
            button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF272727");
        }
        void ClearButtonColor()
        {
            btnInsertCustomer.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF414141");
            btnUpdateCustomer.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF414141");
            btnDeleteCustomer.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF414141");
        }
        public void ClearGrid(DataGrid dataGrid)
        {
            string strCon = Properties.Resources.connection;
            OleDbConnection conn = new OleDbConnection(strCon);
            conn.Open();

            OleDbCommand command = new OleDbCommand("SELECT * FROM customers ORDER BY customerID desc", conn);
            dataGrid.ItemsSource = command.ExecuteReader();
        }
        void showGrid(Grid grid)
        {
            grid.IsEnabled = true;
            grid.Visibility = Visibility.Visible;
        }
        void hideGrid()
        {
            gridInsert.Visibility = Visibility.Hidden;
            gridInsert.IsEnabled = false;

            gridUpdate.Visibility = Visibility.Hidden;
            gridUpdate.IsEnabled = false;

            gridDelete.Visibility = Visibility.Hidden;
            gridDelete.IsEnabled = false;

        }
        void ClearTextBox()
        {
            txtICname.Text = "";
            txtICaddress.Text = "";
            txtICemail.Text = "";

            cbbUCid.SelectedIndex = -1;
            txtUCshowID.Text = "";
            txtUCname.Text = "";
            txtUCaddress.Text = "";
            txtUCemail.Text = "";

            cbbDCid.SelectedIndex = -1;
            txtDCshowID.Text = "";
            txtDCname.Text = "";
            txtDCaddress.Text = "";
            txtDCemail.Text = "";

        }
        public bsCustomer()
        {
            InitializeComponent();
            hideGrid();
            fill_cbb();
            fill_cbb_delete();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            bsMain bsMain = new bsMain();
            this.Visibility = Visibility.Hidden;
            bsMain.Show();
        }

        private void txtICname_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerName = txtICname.Text.Trim();
        }

        private void txtICaddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            address = txtICaddress.Text.Trim();
        }

        private void txtICemail_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = txtICemail.Text.Trim();
        }

        private void txtUCshowID_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerID = txtUCshowID.Text.Trim();
        }

        private void txtUCname_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerName = txtICname.Text.Trim();
        }

        private void txtUCaddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            address = txtICaddress.Text.Trim();
        }

        private void txtUCemail_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = txtICemail.Text.Trim();
        }

        private void txtDCshowID_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerID = txtUCshowID.Text.Trim();
        }

        private void txtDCname_TextChanged(object sender, TextChangedEventArgs e)
        {
            customerName = txtICname.Text.Trim();
        }

        private void txtDCaddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            address = txtICaddress.Text.Trim();
        }

        private void txtDCemail_TextChanged(object sender, TextChangedEventArgs e)
        {
            email = txtICemail.Text.Trim();
        }

        private void btnDCcancel_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }

        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid(dtgMain);
        }

        private void btnInsertCustomer_Click(object sender, RoutedEventArgs e)
        {
            hideGrid();
            showGrid(gridInsert);
            txtICname.IsEnabled = true;
            lblStatus.Content = "Add New Customer";
            txtICname.Focus();
            btnInsertCustomer.Background = Brushes.Black;

            showGrid(gridInsert);
            ClearButtonColor();
            SetColorButton(btnInsertCustomer);
        }

        private void btnUpdateCustomer_Click(object sender, RoutedEventArgs e)
        {
            hideGrid();
            showGrid(gridUpdate);
            lblStatus.Content = "Update or Edit Customer Infomation";
            ClearButtonColor();
            SetColorButton(btnUpdateCustomer);
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            hideGrid();
            showGrid(gridDelete);
            lblStatus.Content = "Delete Customer !!";
            ClearButtonColor();
            SetColorButton(btnDeleteCustomer);
        }

        private void btnICsave_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();

                string strComm = "INSERT INTO customers(customerName, address, email) VALUES ('" + customerName + "','" + address + "','" + email + "');";
                OleDbCommand createComm = new OleDbCommand(strComm, conn);
                createComm.ExecuteNonQuery();

                conn.Close();
                ClearGrid(dtgMain);
                ClearTextBox();
                MessageBox.Show("Insert Success!", "New Customer Added", MessageBoxButton.OK);
                clear_cbb();
                fill_cbb();
                fill_cbb_delete();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btnICcancel_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }

        private void btnUCsave_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                customerID = txtUCshowID.Text;
                customerName = txtUCname.Text;
                address = txtUCaddress.Text;
                email = txtUCemail.Text;

                string strComm = "UPDATE customers SET customerName = '" + customerName + "', address = '" + address + "', email = '" + email + "' WHERE customerID = " + customerID + ";";
                OleDbCommand comm = new OleDbCommand(strComm, conn);
                comm.ExecuteNonQuery();
                ClearTextBox();
                conn.Close();
                MessageBox.Show("Update Success!", "Continue ?", MessageBoxButton.OK);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            ClearGrid(dtgMain);
            clear_cbb();
            fill_cbb();
            fill_cbb_delete();
        }

        private void btnUCcancel_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }

        private void cbbUCid_DropDownClosed(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string Query = "SELECT * FROM customers WHERE customerName = '" + cbbUCid.Text + "' ";
                OleDbCommand createComm = new OleDbCommand(Query, conn);

                OleDbDataReader dataR = createComm.ExecuteReader();
                while (dataR.Read())
                {
                    string id = dataR.GetInt32(0).ToString();
                    string name = dataR.GetString(1);
                    string address = dataR.GetString(2);
                    string email = dataR.GetString(3);

                    txtUCshowID.Text = id;
                    txtUCname.Text = name;
                    txtUCaddress.Text = address;
                    txtUCemail.Text = email;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDCsave_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Comfirm to Delete ?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                OleDbConnection conn = new OleDbConnection(strConn);
                try
                {
                    conn.Open();
                    customerID = txtDCshowID.Text;

                    string strComm = "DELETE FROM customers WHERE customerID = " + customerID + ";";
                    OleDbCommand comm = new OleDbCommand(strComm, conn);
                    comm.ExecuteNonQuery();
                    ClearTextBox();
                    conn.Close();
                    MessageBox.Show("Delete Success!", "Continue ?", MessageBoxButton.OK);
                    ClearGrid(dtgMain);
                    clear_cbb();
                    fill_cbb();
                    fill_cbb_delete();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
        }

        private void btnDCcancel_Click_1(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }

        private void cbbDCid_DropDownClosed(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string Query = "SELECT * FROM customers WHERE customerName = '" + cbbDCid.Text + "' ";
                OleDbCommand createComm = new OleDbCommand(Query, conn);

                OleDbDataReader dataR = createComm.ExecuteReader();
                while (dataR.Read())
                {
                    string id = dataR.GetInt32(0).ToString();
                    string name = dataR.GetString(1);
                    string address = dataR.GetString(2);
                    string email = dataR.GetString(3);

                    txtDCshowID.Text = id;
                    txtDCname.Text = name;
                    txtDCaddress.Text = address;
                    txtDCemail.Text = email;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
