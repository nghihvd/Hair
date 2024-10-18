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
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            currentAccount.Name = txtFullName.Text;
            currentAccount.Email = txtEmail.Text;
            currentAccount.Phone = txtPhoneNumber.Text;
            
            if (!string.IsNullOrEmpty(txtPassword.Password))
            {
                currentAccount.Password = txtPassword.Password;
            }

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
