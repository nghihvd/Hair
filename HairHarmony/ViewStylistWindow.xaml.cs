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
        private readonly IAccountService accountService;
        public ViewStylistWindow()
        {
            InitializeComponent();
            accountService = new AccountService();
            LoadGrid();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeManagerWindow managerWindow = new HomeManagerWindow();
            managerWindow.Show();

        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedIndex < 0) return;
;            DataGridRow row =
                (DataGridRow)dataGrid.ItemContainerGenerator
                .ContainerFromIndex(dataGrid.SelectedIndex);

            DataGridCell? rowColum =
                dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;

            string id = ((TextBlock)rowColum.Content).Text;
            Account ac = accountService.getAccountByID(id);
            this.txtEmail.Text = ac.Email;
            this.txtFullName.Text = ac.Name;
            this.txtPassword.Password = ac.Password;
            this.txtPhoneNumber.Text = ac.Phone;
            this.txtSalary.Text = ac.Salary.ToString();
            this.txtStylistID.Text = ac.AccountId;
            this.cmbLevel.SelectedValue = ac.Level;
        }

        public void LoadGrid()
        {
            this.dtgStylist.ItemsSource = accountService.getStylistAcc().Select(a => new {a.AccountId,a.RoleId,a.Phone,a.Email,a.Level,a.LoyaltyPoints });
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

                || string.IsNullOrEmpty(this.txtFullName.Text))
            {
                MessageBox.Show("Please enter all your information.");
                return;
            }
            if (accountService.getAccountByID(this.txtStylistID.Text) != null)
            {
                MessageBox.Show("Account already exist. Please enter another user name");
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
            account.AccountId = txtStylistID.Text;
            account.Phone = txtPhoneNumber.Text;
            account.RoleId = 2;
            account.Name = txtFullName.Text;
            bool result = accountService.RegisAccount(account);
            if (result)
            {
                MessageBox.Show("Register success");
                return;
            }
            else
            {
                MessageBox.Show("Something wrong please check again.");
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
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            decimal salary;
            if(!decimal.TryParse(this.txtSalary.Text, out salary)){
                MessageBox.Show("Salary is wrong");
                return;
            }
            if (accountService.getAccountByID(this.txtStylistID.Text) != null)
            {
                Account acc = new Account();
                acc.AccountId = this.txtStylistID.Text;
                acc.Salary = Decimal.Parse(this.txtSalary.Text);
                acc.Level = this.cmbLevel.SelectedValue.ToString();
                bool result = accountService.UpdateSylistAcc(acc);
                if (result)
                {
                    MessageBox.Show($"Update account {this.txtStylistID} success.");
                }
                else
                {
                    MessageBox.Show("Something wrong. Please check again.");
                }
            }
            else
            {
                MessageBox.Show("Account not exist");
            }
            this.LoadGrid();
            this.resetInput();
        }

        private void btnEnable_Click(object sender, RoutedEventArgs e)
        {
            bool result = accountService.EnableStylist(this.txtStylistID.Text);
            if (result) {
                MessageBox.Show("Enalble Sucess");
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
    }
}
