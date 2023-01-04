using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for bsBookManage.xaml
    /// </summary>
    public partial class bsBookManage : Window
    {
        string title = "";
        string detail = "";
        string price = "";
        string strConn = Properties.Resources.connection;
        
        string isbn = "";
        
        void fill_cbb()
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
                    cbbUBisbn.Items.Add(n);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        void clear_cbb()
        {
            cbbUBisbn.Items.Clear();
        }
        private void cbbUBisbn_DropDownClosed(object sender, EventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                string Query = "SELECT * FROM books WHERE title = '" + cbbUBisbn.Text + "' "; // ตรงนี้ต้อง Where ชื่อ เพื่อเอาชื่อ ไปเทียบ
                OleDbCommand createComm = new OleDbCommand(Query, conn);
                //สร้างคำสั่ง

                OleDbDataReader dataR = createComm.ExecuteReader();
                while (dataR.Read())
                {
                    string ISBN = dataR.GetInt32(0).ToString();
                    string title = dataR.GetString(1);
                    string description = dataR.GetString(2);
                    string price = dataR.GetString(3);

                    txtUBshowISBN.Text = ISBN;
                    txtUBname.Text = title;
                    txtUBprice.Text = price;
                    txtUBdescp.Text = description;


                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        void SelectText(TextBox tb)
        {
            tb.SelectionStart = 0;
            tb.SelectionLength = tb.Text.Length;

        }
        void SetColorButton(Button button)
        {
            button.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF272727");
        }
        void ClearButtonColor()
        {
            btnInsertBook.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF414141");
            btnUpdateBook.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF414141");
            btnDeleteBook.Background = (SolidColorBrush)new BrushConverter().ConvertFromString("#FF414141");
        }
        public void ClearGrid(DataGrid dataGrid)
        {
            string strCon = Properties.Resources.connection;
            OleDbConnection conn = new OleDbConnection(strCon);
            conn.Open();

            OleDbCommand command = new OleDbCommand("SELECT * FROM books ORDER BY ISBN desc", conn);
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

            //gridDelete.Visibility = Visibility.Hidden;
            //gridDelete.IsEnabled = false;

        }
        void ClearTextBox()
        {
            txtIBname.Text = "";
            txtIBdescp.Text = "";
            txtIBprice.Text = "";

            cbbUBisbn.SelectedIndex = -1;
            txtUBshowISBN.Text = "";
            txtUBname.Text = "";
            txtUBdescp.Text = "";
            txtUBprice.Text = "";

            //txtDBfullname.Text = "";
            //txtDBaddress.Text = "";
            //txtDBemail.Text = "";
        }

        public bsBookManage()
        {
            InitializeComponent();
            hideGrid();
            fill_cbb();
            
        }
        private void txtIBname_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectText(txtIBname);
        }

        private void txtIBprice_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectText(txtIBprice);
        }

        private void txtIBdescp_GotMouseCapture(object sender, MouseEventArgs e)
        {
            SelectText(txtIBdescp);
        }

        private void txtIBname_TextChanged(object sender, TextChangedEventArgs e)
        {
            title = txtIBname.Text.Trim();
        }

        private void txtIBprice_TextChanged(object sender, TextChangedEventArgs e)
        {
            price = txtIBprice.Text.Trim();
        }

        private void txtIBdescp_TextChanged(object sender, TextChangedEventArgs e)
        {
            detail = txtIBdescp.Text.Trim();
        }
        private void txtUBname_TextChanged(object sender, TextChangedEventArgs e)
        {
            title = txtIBname.Text.Trim();
        }

        private void txtUBprice_TextChanged(object sender, TextChangedEventArgs e)
        {
            price = txtIBname.Text.Trim();
        }

        private void txtUBdescp_TextChanged(object sender, TextChangedEventArgs e)
        {
            detail = txtIBname.Text.Trim();
        }
        private void txtUBshowISBN_TextChanged(object sender, TextChangedEventArgs e)
        {
            isbn = txtUBshowISBN.Text.Trim();
        }



        private void btnIBcancel_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            bsMain bsMain = new bsMain();
            this.Visibility = Visibility.Hidden;
            bsMain.Show();
        }
        private void btnRefresh_Click(object sender, RoutedEventArgs e)
        {
            ClearGrid(dtgMain);
        }

        private void btnInsertBook_Click(object sender, RoutedEventArgs e)
        {
            hideGrid();
            showGrid(gridInsert);
            txtIBname.IsEnabled = true;
            lblStatus.Content = "Insert Menu";
            txtIBname.Focus();
            btnInsertBook.Background = Brushes.Black;

            showGrid(gridInsert);
            ClearButtonColor();
            SetColorButton(btnInsertBook);
        }

        private void btnUpdateBook_Click(object sender, RoutedEventArgs e)
        {
            hideGrid();
            showGrid(gridUpdate);
            lblStatus.Content = "Update or Edit Book Infomation";
            ClearButtonColor();
            SetColorButton(btnUpdateBook);
        }

        private void btnDeleteBook_Click(object sender, RoutedEventArgs e)
        {
            hideGrid();
            //showGrid(gridDelete);
            lblStatus.Content = "Delete Book !!";
            ClearButtonColor();
            SetColorButton(btnDeleteBook);
        }

        private void btnIBsave_Click(object sender, RoutedEventArgs e) //อย่าลืม ใช้ตัวนี้ต้องมี textChange ที่แต่ละ class ของ textbox ด้วย
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();

                string strComm = "INSERT INTO books(title, description, price) VALUES ('" + title + "','" + detail + "','" + price + "');";
                OleDbCommand createComm = new OleDbCommand(strComm, conn);
                createComm.ExecuteNonQuery();

                conn.Close();
                ClearGrid(dtgMain);
                ClearTextBox();
                MessageBox.Show("Insert Success!", "New Book Added", MessageBoxButton.OK);
                clear_cbb();
                fill_cbb();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        

        private void btnUBsave_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn);
            try
            {
                conn.Open();
                isbn = txtUBshowISBN.Text;
                title = txtUBname.Text;
                detail = txtUBdescp.Text;
                price = txtUBprice.Text;

                string strComm = "UPDATE books SET title = '" + title + "', description = '" + detail + "', price = '" + price + "' WHERE ISBN = " + isbn + ";";
                OleDbCommand comm = new OleDbCommand(strComm, conn);
                //comm.Connection = conn;
                //comm.CommandType = CommandType.Text;
                //comm.CommandText = strComm;
                comm.ExecuteNonQuery();

                ClearTextBox();

                conn.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            ClearGrid(dtgMain);
            clear_cbb();
            fill_cbb();
        }



        private void btnUBcancel_Click(object sender, RoutedEventArgs e)
        {
            ClearTextBox();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            cbbUBisbn.Items.Clear();
        }
    }
    
}
