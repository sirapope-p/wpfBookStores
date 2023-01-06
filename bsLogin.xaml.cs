using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Windows.Input;

namespace wpfBookStores
{
    /// <summary>
    /// Interaction logic for bsLogin.xaml
    /// </summary>
    public partial class bsLogin : Window
    {
        void SelectText(TextBox tb)
        {
            tb.SelectionStart = 0;
            tb.SelectionLength = tb.Text.Length;
        }
        void DeniteStatus()
        {
            lblChat.Content = "Can't Accept! Please Enter Your Username and Password.";
            lblChat.FontSize = 18;
            iconCA.Visibility = Visibility;
        }
        void bsMainOpen()
        {
            bsMain bsMain = new bsMain();
            this.Visibility = Visibility.Hidden;
            bsMain.Show();
        }

        string strConn = Properties.Resources.connection; // 1.สร้างตัวแปร String เก็บค่าจาก connection(path ของฐานข้อมูลที่สร้างเก็บไว้ใน Resources.resx)
        public bsLogin()
        {
            InitializeComponent();
            txtUserInPut.Focus();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            OleDbConnection conn = new OleDbConnection(strConn); // 2.สร้าง oledbConnection และกำหนดให้รับค่า String จากข้อ 1 

            try
            {
                if (cbUseGuestAcc.IsChecked == true)
                {
                    bsMainOpen();
                }
                else
                {
                    conn.Open();

                    string strComm = "SELECT * FROM login WHERE userName='" + txtUserInPut.Text + "' and passWord='" + txtPasswordInPut.Text + "' and passOut=0;";
                    OleDbCommand createComm = new OleDbCommand(strComm, conn);

                    OleDbDataReader reader = createComm.ExecuteReader();
                    int count = 0;
                    while (reader.Read())
                    {
                        count = count + 1;
                    }
                    if (count == 1)
                    {
                        bsMainOpen();
                    }
                    else if (count > 1)
                    {
                        MessageBox.Show("Duplicate Username and Password. Contact Admin for More Infomation");
                        DeniteStatus();
                    }
                    else
                    {
                        MessageBoxResult result = MessageBox.Show("Username and Password is not Correct", "Can't Accept!", MessageBoxButton.OK, MessageBoxImage.Error);
                        DeniteStatus(); 

                        if (result == MessageBoxResult.OK)
                        {
                            txtUserInPut.Focus();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cbUseGuestAcc_Checked(object sender, RoutedEventArgs e)
        {
            txtUserInPut.Text = "GuestA20230101";
            txtPasswordInPut.Text = "********";
            txtUserInPut.IsEnabled = false;
            txtPasswordInPut.IsEnabled = false;
            lblChat.Content = "Login Now. You checkin with Guest ID.";
        }

        private void cbUseGuestAcc_Unchecked(object sender, RoutedEventArgs e)
        {
            txtUserInPut.Text = "Username";
            txtPasswordInPut.Text = "Pssword";
            txtUserInPut.IsEnabled = true;
            txtPasswordInPut.IsEnabled = true;
            lblChat.Content = "Please Login with Your Account.";
        }


        private void txtUserInPut_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectText(txtUserInPut);
        }

        private void txtPasswordInPut_GotFocus(object sender, RoutedEventArgs e)
        {
            SelectText(txtPasswordInPut);
        }

        private void txtUserInPut_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TraversalRequest request = new TraversalRequest(FocusNavigationDirection.Next);
                request.Wrapped = true;
                ((TextBox)sender).MoveFocus(request);
            }
        }

        private void txtPasswordInPut_PreviewKeyDown(object sender, KeyEventArgs e)
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
