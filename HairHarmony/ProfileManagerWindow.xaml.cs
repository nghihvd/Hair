using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HairHarmony_BusinessObject;
using HairHarmony_Repository;
using HairHarmony_Services;

namespace PRN212_HairHarmony
{
    /// <summary>
    /// Interaction logic for ProfileManagerWindow.xaml
    /// </summary>
    public partial class ProfileManagerWindow : Window
    {
        private readonly IAccountService iaccountServ;
        private Account? accountLogged;
        //public ProfileManagerWindow()
        //{
        //    InitializeComponent();
        //    iaccountServ = new AccountService();
        //    loadInit();
        //}
        public ProfileManagerWindow(Account account)
        {
            InitializeComponent();
            iaccountServ = new AccountService();
            accountLogged = account;
            loadInit();
            
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeManagerWindow HomeManagerWindow = new HomeManagerWindow();
            HomeManagerWindow.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void loadInit()
        {
            Account accountSearched = iaccountServ.getAccountByID(accountLogged.AccountId);
            if (accountSearched != null)
            {
                txtAccountId.Text = accountSearched.AccountId;
                txtEmail.Text = accountSearched.Email;
                txtFullName.Text = accountSearched.Name;
                txtPhoneNumber.Text = accountSearched.Phone;
                txtPasswordNew.Password = null;
                txtConfirmPassword.Password = null;
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {   
           Account accountSearched = iaccountServ.getAccountByID(accountLogged.AccountId);
            if (!txtPhoneNumber.Text.StartsWith("0") || txtPhoneNumber.Text.Length >12 || txtPhoneNumber.Text.Length < 9)
            {
                MessageBox.Show("Your Phone Number is not real","Notification",MessageBoxButton.OK,MessageBoxImage.Warning);
                return;
            }

            //kiểm tra '@' 
            //bắt đầu kiểm tra '^'
            //trước '@' không có bất kì '@' nào 
            //sau '@' cũng vậy 
            //bắt buộc có '.' 
            //và sau '.' không có '@' 
            //kết thúc kiểm tra '$'
            //            if (!Regex.IsMatch(txtEmail.Text,@"^[a-zA-Z0-9._]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$"))

            if (!Regex.IsMatch(txtEmail.Text,@"^[a-zA-Z0-9._]+@[a-zA-Z0-9]+(\.[a-zA-Z]{2,})+$"))
            {
                MessageBox.Show("Your Email wrong format!!");
                return;
            }
            if (accountSearched != null)
            {
                MessageBoxResult mbr = MessageBox.Show("Are you sure to update?", "Notification", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mbr == MessageBoxResult.Yes)
                {
                    // Yêu cầu người dùng nhập mật khẩu cũ
                    string inputPassword = Microsoft.VisualBasic.Interaction.InputBox("Please enter your current password:", "Password Confirmation", "");
                    if (inputPassword == accountSearched.Password) // Giả sử mật khẩu được lưu trong thuộc tính Password
                    {
                        if (!string.IsNullOrEmpty(txtFullName.Text))
                        {
                            accountSearched.Name = txtFullName.Text;
                        }
                        if (!string.IsNullOrEmpty(txtPhoneNumber.Text))
                        {
                            accountSearched.Phone = txtPhoneNumber.Text;
                        }
                        if (!string.IsNullOrEmpty(txtEmail.Text)) 
                        {
                            accountSearched.Email = txtEmail.Text;

                        }
                        if (!string.IsNullOrEmpty(txtPasswordNew.Password))
                        {
                            
                            if (txtConfirmPassword.Password != txtPasswordNew.Password)
                            {
                                MessageBox.Show("Your confirm password wrong!", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                                return;
                            }
                            else
                            {
                                accountSearched.Password = txtPasswordNew.Password;
                            }
                        }

                        bool updateAcc = iaccountServ.UpdateManagerAcc(accountSearched);
                        if (updateAcc)
                        {
                            MessageBox.Show("Successfully Update!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                            loadInit();
                        }
                        else
                        {
                            MessageBox.Show("Failed Update!", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Incorrect password. Update canceled.", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }

                }
            }
        }
    }
}
