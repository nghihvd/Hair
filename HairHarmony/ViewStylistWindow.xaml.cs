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
    /// Interaction logic for ViewStylistWindow.xaml
    /// </summary>
    public partial class ViewStylistWindow : Window
    {
        private Account? acc = null;
        private readonly IAccountService accountService;
        public ViewStylistWindow()
        {
            InitializeComponent();
            accountService = new AccountService();
            LoadGrid();
        }
        public ViewStylistWindow(Account? acc)
        {
            this.acc = acc;
            InitializeComponent();
            accountService = new AccountService();
            btnAdd.Visibility = Visibility.Hidden;
            btnEnable.Visibility = Visibility.Hidden;
            btnDetail.Visibility = Visibility.Hidden;
            btnUpdate.Visibility = Visibility.Hidden;
            tblSalary.Visibility = Visibility.Hidden;
            txtSalary.Visibility = Visibility.Hidden;
            txtPassword.Visibility = Visibility.Hidden;
            tblPass.Visibility = Visibility.Hidden;
            LoadGridMember();
        }

        private void LoadGridMember()
        {
            dtgStylist.ItemsSource = accountService.getStylistAcc().Select(a => new {a.AccountId,a.RoleId,a.LoyaltyPoints,a.Phone });
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            if (acc != null)
            {
                this.Hide();
                HomeWindow homeWindow = new HomeWindow();
                homeWindow.Show();
                return;
            }
            this.Hide();
            HomeManagerWindow managerWindow = new HomeManagerWindow();
            managerWindow.Show();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedIndex < 0) return;
            DataGridRow row =
                            (DataGridRow)dataGrid.ItemContainerGenerator
                            .ContainerFromIndex(dataGrid.SelectedIndex);
            if (row == null)
            {
                return;
            }
            DataGridCell? rowColum =
                dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
            if (rowColum == null) return;
            string id = ((TextBlock)rowColum.Content).Text;
            if (id == null) return;
            Account ac = accountService.getAccountByID(id);
            if (ac == null) return;
            this.txtEmail.Text = ac.Email;
            this.txtFullName.Text = ac.Name;
            this.txtPassword.Password = ac.Password;
            this.txtPhoneNumber.Text = ac.Phone;
            this.txtSalary.Text = ac.Salary.ToString();
            this.txtStylistID.Text = ac.AccountId;
            this.cmbLevel.SelectedValue = ac.Level;
            this.txtPoints.Text = ac.LoyaltyPoints.ToString();
        }

        private void LoadGrid()
        {
            this.dtgStylist.ItemsSource = accountService.getStylistAcc().Select(a => new { a.AccountId, a.RoleId, a.Phone, a.Email, a.Level, a.LoyaltyPoints, a.Salary });
            List<string> level = new List<string> { "Junior Stylist", "Stylist", "Senior Stylist", "Master Stylist", "Creative Director" };
            this.cmbLevel.ItemsSource = level;

        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            int role = 0;

            if (string.IsNullOrEmpty(this.txtStylistID.Text)
                || string.IsNullOrEmpty(this.txtPassword.Password)
                || string.IsNullOrEmpty(this.txtPhoneNumber.Text)
                || string.IsNullOrEmpty(this.txtEmail.Text)

                || string.IsNullOrEmpty(this.txtFullName.Text) ||
                cmbLevel.SelectedItem == null)
            {
                MessageBox.Show("Please enter all information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (accountService.getAccountByID(this.txtStylistID.Text) != null)
            {
                MessageBox.Show("Account already exist. Please enter another user name",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            string pattern = @"^0\d{8,10}$";
            if (!Regex.IsMatch(this.txtPhoneNumber.Text, pattern))
            {
                MessageBox.Show("Phone number has to 9 to 11 numbers and start with 0",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            string patternEmail = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            if (!Regex.IsMatch(this.txtEmail.Text, patternEmail))
            {
                MessageBox.Show("Wrong pattern email", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

                return;
            }

            MessageBoxResult messageBox = MessageBox.Show("Do you want to add " + this.txtFullName.Text + " to system?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBox == MessageBoxResult.Yes)
            {
                Account account = new Account();
                account.Password = txtPassword.Password;
                account.Email = txtEmail.Text;
                account.AccountId = txtStylistID.Text;
                account.Phone = txtPhoneNumber.Text;
                account.RoleId = 2;
                account.Name = txtFullName.Text;
                if (string.IsNullOrEmpty(txtSalary.Text))
                {
                    account.Salary = 0;
                }
                else
                {
                    account.Salary = int.Parse(txtSalary.Text.ToString());
                }
                account.Level = cmbLevel.SelectedValue.ToString();
                account.LoyaltyPoints = 0;
                bool result = accountService.RegisAccount(account);
                if (result)
                {
                    MessageBox.Show("Register success", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("Something wrong please check again.", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            resetInput();
            LoadGrid();

        }
        private void resetInput()
        {
            this.txtStylistID.Text = "";
            this.txtSalary.Text = "";
            this.txtPoints.Text = "";
            this.txtPassword.Password = "";
            this.txtFullName.Text = "";
            this.txtEmail.Text = "";
            this.txtPhoneNumber.Text = "";
            this.cmbLevel.SelectedValue = null;
            this.dtgStylist.SelectedItem = null;
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtStylistID.Text)
                || string.IsNullOrEmpty(this.txtPassword.Password)
                || string.IsNullOrEmpty(this.txtPhoneNumber.Text)
                || string.IsNullOrEmpty(this.txtEmail.Text)

                || string.IsNullOrEmpty(this.txtFullName.Text))
            {
                MessageBox.Show("Please enter all information.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            decimal salary;
            if (!decimal.TryParse(this.txtSalary.Text, out salary))
            {
                MessageBox.Show("Salary is wrong", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            if (accountService.getAccountByID(this.txtStylistID.Text) != null)
            {
                Account acc = new Account();
                acc.AccountId = this.txtStylistID.Text;

                if (this.txtSalary.Text.Trim().Length == 0)
                {
                    acc.Salary = 0;
                }
                else
                {
                    acc.Salary = Decimal.Parse(this.txtSalary.Text);
                }
                if (this.cmbLevel.SelectedValue == null)
                {
                    acc.Level = null;
                }
                else
                {
                    acc.Level = this.cmbLevel.SelectedValue.ToString();
                }

                bool result = accountService.UpdateSylistAcc(acc);
                if (result)
                {
                    MessageBox.Show($"Update account {this.txtStylistID.Text} success.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Something wrong. Please check again.", "Error", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            else
            {
                MessageBox.Show("Account not exist", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.LoadGrid();
            this.resetInput();
        }

        private void btnEnable_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtStylistID.Text))
            {
                MessageBox.Show("Please enter stylist ID", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            bool result = accountService.EnableStylist(this.txtStylistID.Text);
            if (result)
            {
                MessageBox.Show("Disable Success");
                this.LoadGrid();
                this.resetInput();
            }
            else
            {
                MessageBox.Show("Please check stylistID");
                this.resetInput();
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            resetInput();
        }

        private void btnDetail_Click(object sender, RoutedEventArgs e)
        {

            this.Hide();
            ViewComissionRate viewComissionRate = new ViewComissionRate();
            viewComissionRate.Show();
        }

    }
}
