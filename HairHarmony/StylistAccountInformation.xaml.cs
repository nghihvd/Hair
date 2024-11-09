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
    /// Interaction logic for StylistAccountInformation.xaml
    /// </summary>
    public partial class StylistAccountInformation : Window
    {
        private readonly IAccountService accountService;
        private Account? account;
        public StylistAccountInformation(Account account)
        {
            InitializeComponent();
            accountService = new AccountService();
            loadProfile();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();

        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeStylistWindow stylistWindow = new HomeStylistWindow();
            stylistWindow.Show();
        }
        private void loadProfile()
        {
            var account = Application.Current.Properties["LoggedAccount"] as Account;

            if (account != null)
            {
                txtFullName.Text = account.Name;
                txtEmail.Text = account.Email;
                txtPhoneNumber.Text = account.Phone;
                txtPasswordNew.Password = null;
                txtConfirmPassword.Password = null;
                txtStylistId.Text = account.AccountId;
                txtPoint.Text = account.LoyaltyPoints.ToString();
                txtLevel.Text = account.Level;
                txtSalary.Text = account.Salary.ToString();

            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var account = Application.Current.Properties["LoggedAccount"] as Account;
            if (!txtPhoneNumber.Text.StartsWith("0") || txtPhoneNumber.Text.Length > 12 || txtPhoneNumber.Text.Length < 9)
            {
                MessageBox.Show("Your Phone Number is not real", "Notification", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (!Regex.IsMatch(txtEmail.Text, @"^[a-zA-Z0-9._]+@[a-zA-Z0-9]+(\.[a-zA-Z]{2,})+$"))
            {
                MessageBox.Show("Your Email's format is wrong!!");
                return;
            }
            if (account != null)
            {
                MessageBoxResult mbr = MessageBox.Show("Are you sure to update?", "Notification", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (mbr == MessageBoxResult.Yes)
                {
                    // Yêu cầu người dùng nhập mật khẩu cũ
                    string inputPassword = Microsoft.VisualBasic.Interaction.InputBox("Please enter your current password:", "Password Confirmation", "");
                    if (inputPassword == account.Password) // Giả sử mật khẩu được lưu trong thuộc tính Password
                    {
                        if (!string.IsNullOrEmpty(txtFullName.Text))
                        {
                            account.Name = txtFullName.Text;
                        }
                        if (!string.IsNullOrEmpty(txtPhoneNumber.Text))
                        {
                            account.Phone = txtPhoneNumber.Text;
                        }
                        if (!string.IsNullOrEmpty(txtEmail.Text))
                        {
                            account.Email = txtEmail.Text;

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
                                account.Password = txtPasswordNew.Password;
                            }
                        }

                        bool updateAcc = accountService.UpdateManagerAcc(account);
                        if (updateAcc)
                        {
                            MessageBox.Show("Successfully Update!", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
                            loadProfile();
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
