using HairHarmony_BusinessObject;
using HairHarmony_Services;
using System.Windows;

namespace PRN212_HairHarmony
{
    public partial class ProfileWindow : Window
    {
        private readonly IAccountService accountService;
        private Account currentAccount;

        public ProfileWindow()
        {
            InitializeComponent();
            accountService = new AccountService();
            LoadProfile();
        }

        private void LoadProfile()
        {
            currentAccount = (Account)Application.Current.Properties["LoggedAccount"];

            if (currentAccount != null)
            {
                txtAccountId.Text = currentAccount.AccountId.ToString();
                txtFullName.Text = currentAccount.Name;
                txtEmail.Text = currentAccount.Email;
                txtPhoneNumber.Text = currentAccount.Phone;
                txtPoint.Text = currentAccount.LoyaltyPoints + "";
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            string inputPassword = Microsoft.VisualBasic.Interaction.InputBox("Please enter your current password:", "Password Confirmation", "");

            if (inputPassword != currentAccount.Password)
            {
                MessageBox.Show("Current password is incorrect!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (!string.IsNullOrEmpty(txtPassword.Password))
            {
                if (txtPassword.Password != txtConfirmPassword.Password)
                {
                    MessageBox.Show("New password and confirm password do not match!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                currentAccount.Password = txtPassword.Password;
            }
            currentAccount.Name = txtFullName.Text;
            currentAccount.Email = txtEmail.Text;
            currentAccount.Phone = txtPhoneNumber.Text;

            accountService.UpdateAccount(currentAccount);

            MessageBox.Show("Account updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }


        private void btnGoHome_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeWindow homeWindow = new HomeWindow();
            homeWindow.Show();
        }
    }
}
