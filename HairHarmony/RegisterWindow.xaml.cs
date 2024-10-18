using HairHarmony_BusinessObject;
using HairHarmony_Services;
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

namespace PRN212_HairHarmony
{
    /// <summary>
    /// Interaction logic for RegisterWindow.xaml
    /// </summary>
    public partial class RegisterWindow : Window
    {
        private IAccountService accountService;
        public RegisterWindow()
        {
            InitializeComponent();
            accountService = new AccountService();
        }





        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            int role = 0;

            if (string.IsNullOrEmpty(this.txtUsername.Text)
                || string.IsNullOrEmpty(this.txtPassword.Password)
                || string.IsNullOrEmpty(this.txtConfirmPassword.Password)
                || string.IsNullOrEmpty(this.txtPhoneNumber.Text)
                || string.IsNullOrEmpty(this.txtEmail.Text)

                || string.IsNullOrEmpty(this.txtFullName.Text))
            {
                MessageBox.Show("Please enter all your information.");
                return;
            }
            if (accountService.getAccountByID(this.txtUsername.Text) != null)
            {
                MessageBox.Show("Account already exist. Please enter another user name");
                return;
            }

            if (!txtConfirmPassword.Password.Equals(txtPassword.Password))
            {
                MessageBox.Show("Confirm password is incorrect");
                return;
            }
            string pattern = @"^0\d{8,10}$";
            if (!Regex.IsMatch(this.txtPhoneNumber.Text, pattern))
            {
                MessageBox.Show("Phone number has to 9 to 11 numbers and start with 0");
                return;
            }

            Account account = new Account();
            account.Password = txtPassword.Password;
            account.Email = txtEmail.Text;
            account.AccountId = txtUsername.Text;
            account.Phone = txtPhoneNumber.Text;
            account.RoleId = 3;
            account.Name = txtFullName.Text;
            bool result = accountService.RegisAccount(account);
            if (result)
            {
                MessageBox.Show("Register success");
                return;
            }
            else { MessageBox.Show("Something wrong please check again."); }

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow login = new LoginWindow();
            login.Show();
        }
    }
}
