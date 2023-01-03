using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace wpfBookStores
{
    /// <summary>
    /// Interaction logic for bsLogin.xaml
    /// </summary>
    public partial class bsLogin : Window
    {
        //int id; ไม่น่าได้ใช้
        string userName = "";
        string passWord = "";
        int passOut;
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

                    // Backup "INSERT INTO login(ID, userName, passWord, passOut) VALUES ('" + userName + "','" + passWord + "','" + passOut + "');"
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
                        MessageBox.Show("Username and Password is Correct");
                        bsMainOpen();
                    }
                    else if (count > 1)
                    {
                        MessageBox.Show("Duplicate Username and Password");
                    }
                    else
                    {
                        MessageBox.Show("Username and Password is not Correct");
                        DeniteStatus();
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
            txtUserInPut.IsEnabled = false;
            txtPasswordInPut.IsEnabled = false;
            lblChat.Content = "Login Now. You checkin with Guest ID.";
        }
    }
}
