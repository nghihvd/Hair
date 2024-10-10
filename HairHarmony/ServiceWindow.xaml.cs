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
        public ServiceWindow()
        {
            InitializeComponent();
            serviceService = new ServiceService();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeManagerWindow homeWindow = new HomeManagerWindow();
            homeWindow.Show();
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
            Service service = new Service();
            this.txtServiceID.Text = service.ServiceId.ToString();
            this.txtServiceName.Text = service.ServiceName;
            this.txtPrice.Text = service.Price.ToString();
            this.txtDuration.Text = service.Duration.ToString();
        }

        private void LoadGrid()
        {
            this.dtgService.ItemsSource = serviceService.GetServiceList();
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
            if(!int.TryParse(this.txtServiceID.Text, out serviceID))
            {
                MessageBox.Show("Invalid ID, ID is number");
                return;
            }

            decimal price;
            if (!int.TryParse(this.txtServiceID.Text, out serviceID))
            {
                MessageBox.Show("Invalid ID, ID is number");
                return;
            }
            Service service = new Service();
            service.Price = this.
        }
    }

}
