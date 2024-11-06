using HairHarmony_BusinessObject;
using HairHarmony_Services;
using System;
using System.Collections.Generic;
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

namespace PRN212_HairHarmony
{
    /// <summary>
    /// Interaction logic for Service.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        private readonly IServiceService serviceService;
        private readonly IStylistServiceService stylistService;
        private int? role;
        private Account? account;
        public ServiceWindow()
        {
            InitializeComponent();
            serviceService = new ServiceService();
            stylistService = new StylistServiceService();
            account = Application.Current.Properties["LoggedAccount"] as Account;
            LoadGrid();
        }
        public ServiceWindow(int? role)
        {
            InitializeComponent();
            serviceService = new ServiceService();
            stylistService = new StylistServiceService();
            account = Application.Current.Properties["LoggedAccount"] as Account;
            switch (role)
            {
                case 1:

                    this.btnSelect.Visibility = Visibility.Hidden;
                    break;
                case 2:
                    this.btnAdd.Visibility = Visibility.Hidden;
                    this.btnDelete.Visibility = Visibility.Hidden;
                    this.btnUpdate.Visibility = Visibility.Hidden;

                    break;
            }
            LoadGrid();
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            switch (account.RoleId)
            {
                case 1:
                    this.Hide();
                    HomeManagerWindow homeWindow = new HomeManagerWindow();
                    homeWindow.Show();
                    break;
                case 2:
                    this.Hide();
                    ViewComissionRate viewComissionRate = new ViewComissionRate();
                    viewComissionRate.Show();
                    break;

            }

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

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedIndex < 0)
            {
                return;
            }
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);

            DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

            string id = ((TextBlock)cell.Content).Text;
            int serviceID = int.Parse(id);
            Service service = serviceService.GetServiceByID(serviceID);
            this.txtServiceID.Text = service.ServiceId.ToString();
            this.txtServiceName.Text = service.ServiceName;
            this.txtPrice.Text = service.Price.ToString();
            this.txtDuration.Text = service.Duration.ToString();
        }

        private void LoadGrid()
        {
            this.dtgService.ItemsSource = serviceService.GetServiceList().Select(a => new { a.ServiceId, a.ServiceName });
        }

        private void ResetInput()
        {
            this.txtDuration.Text = "";
            this.txtPrice.Text = "";
            this.txtServiceID.Text = "";
            this.txtServiceName.Text = "";
        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(this.txtDuration.Text)
                || string.IsNullOrEmpty(this.txtServiceName.Text)
                || string.IsNullOrEmpty(this.txtPrice.Text)
                || string.IsNullOrEmpty(this.txtServiceID.Text))
            {
                MessageBox.Show("Please enter enough information");
                return;
            }
            int serviceID;
            if (!int.TryParse(this.txtServiceID.Text, out serviceID))
            {
                MessageBox.Show("Invalid ID, ID is number");
                return;
            }

            decimal price;
            if (!decimal.TryParse(this.txtPrice.Text, out price))
            {
                MessageBox.Show("Invalid price,price is number");
                return;
            }
            Service service = new Service();
            service.Price = decimal.Parse(this.txtPrice.Text);
            service.Duration = int.Parse(this.txtDuration.Text);
            service.ServiceId = int.Parse(this.txtServiceID.Text);
            service.ServiceName = this.txtServiceName.Text;
            bool result = serviceService.AddService(service);
            if (result)
            {
                MessageBox.Show("Add service success!!");
                LoadGrid();
                ResetInput();
            }
            else
            {
                MessageBox.Show("Some thing wrong");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {

            Service service = new Service();
            service.Price = decimal.Parse(this.txtPrice.Text);
            service.Duration = int.Parse(this.txtDuration.Text);
            service.ServiceName = this.txtServiceName.Text;
            service.ServiceId = int.Parse(this.txtServiceID.Text);
            if (serviceService.GetServiceByID(service.ServiceId) == null)
            {
                MessageBox.Show("Id not exit.Please enter new ID");
                return;
            }
            bool result = serviceService.UpdateService(service);
            if (result)
            {
                MessageBox.Show("Update success");
                LoadGrid();
                ResetInput();
            }
            else
            {
                MessageBox.Show("Something wrong.Please check again!!!!");
            }

        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int id = int.Parse(this.txtServiceID.Text);
            bool result = serviceService.DeleteService(id);
            if (result)
            {
                MessageBox.Show("Delete success!!");
                LoadGrid();
                ResetInput();
            }
            else
            {
                MessageBox.Show("Something wrong");
            }
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            ResetInput();
        }

        private void btnSelect_Click(object sender, RoutedEventArgs e)
        {
          
            if (txtDuration.Text == null ||
                txtPrice.Text == null ||
                txtServiceID.Text == null ||
                txtServiceName.Text == null)
            {
                MessageBox.Show("All field is required.");
                return;
            }
            int id = int.Parse(txtServiceID.Text);
            if(serviceService.GetServiceByID(id) == null)
            {
                MessageBox.Show("Service not exist.");
                return;
            }
            StylistService stylistPerService = new StylistService();
            stylistPerService.StylistId = account.AccountId;
            stylistPerService.Status = true;
            stylistPerService.CommissionRate = 0;
            stylistPerService.ServiceId = id;
            
            bool result = stylistService.AddMoreServiceOfStylist(stylistPerService);
            if (result)
            {
                MessageBox.Show("Add success");

            }
            else
            {
                MessageBox.Show("Service already added");
            }
        }

    }
}
