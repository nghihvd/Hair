using HairHarmony_BusinessObject;
using HairHarmony_Repository;
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
    /// Interaction logic for ViewComissionRate.xaml
    /// </summary>
    public partial class ViewComissionRate : Window
    {
        private readonly IStylistServiceService stylistServiceService;
        private Account? account;
        public ViewComissionRate()
        {
            InitializeComponent();
            stylistServiceService = new StylistServiceService();
            account = Application.Current.Properties["LoggedAccount"] as Account;

            switch (account.RoleId)
            {
                case 1:
                    btnAddService.Visibility = Visibility.Hidden;
                    btnEnableService.Visibility = Visibility.Hidden;
                    btnDisable.Visibility = Visibility.Hidden;
                    this.dtgService.ItemsSource = stylistServiceService.getListServiceStylist();
                    break;
                case 2:
                    btnComission.Visibility = Visibility.Hidden;
                    LoadGrid();
                    break;
            }
        }
        public ViewComissionRate(string id)
        {
            InitializeComponent();
            stylistServiceService = new StylistServiceService();
            account = Application.Current.Properties["LoggedAccount"] as Account;

            switch (account.RoleId)
            {
                case 1:
                    btnAddService.Visibility = Visibility.Hidden;
                    btnEnableService.Visibility = Visibility.Hidden;
                    btnDisable.Visibility = Visibility.Hidden;
                    this.dtgService.ItemsSource = stylistServiceService.getListServiceStylist();
                    break;
                case 2:
                    btnComission.Visibility = Visibility.Hidden;
                    LoadGrid();
                    break;
            }

        }


        public void LoadGrid()
        {
            this.dtgService.ItemsSource = stylistServiceService.GetStylistServiceByStylistID(account.AccountId);
        }
        public void LoadGridAll()
        {
            this.dtgService.ItemsSource = stylistServiceService.getListServiceStylist();
        }


        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row =
                (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);

            DataGridCell cell =
                dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

            string id = ((TextBlock)cell.Content).Text; // cột đầu tiên là cột stylistID

            DataGridCell cell2 =
                dataGrid.Columns[1].GetCellContent(row).Parent as DataGridCell;
            string serviceId = ((TextBlock)cell2.Content).Text;
            int ser = int.Parse(serviceId);
            StylistService stylistService = stylistServiceService.GetStylistServiceByStylistIDAndServiceID(id, ser);
            this.txtServiceID.Text = stylistService.ServiceId.ToString();
            this.txtComission.Text = stylistService.CommissionRate.ToString();
            this.ckbRate.IsChecked = stylistService.Status;
            this.tblStylistID.Text = stylistService.StylistId;

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            switch (account.RoleId)
            {
                case 1:
                    this.Hide();
                    ViewStylistWindow viewStylistWindow = new ViewStylistWindow();
                    viewStylistWindow.Show();
                    break;
                case 2:
                    this.Hide();
                    HomeStylistWindow homeStylistWindow = new HomeStylistWindow();
                    homeStylistWindow.Show();
                    break;
            }
            
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            switch (account.RoleId)
            {
                case 1:
                    LoadGridAll();
                    break;
                case 2:
                    LoadGrid();
                    break;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnAddService_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ServiceWindow serviceWindow = new ServiceWindow(2);
            serviceWindow.Show();
        }

        private void btnDisable_Click(object sender, RoutedEventArgs e)
        {
            if (txtComission.Text == null ||
               txtServiceID.Text == null)
            {
                MessageBox.Show("All field is required");
                return;
            }
            bool ser = int.TryParse(this.txtServiceID.Text, out int serviceID);
            if (!ser)
            {
                MessageBox.Show("Invalid serviceID");
                return;
            }
            var stylist = Application.Current.Properties["LoggedAccount"] as Account;
            bool result = stylistServiceService.DisableServiceStylist(stylist.AccountId, serviceID);
            if (result)
            {
                MessageBox.Show("Disable success");

            }
            else { MessageBox.Show("Already disable service."); }
            LoadGrid();
            resetInput();
        }

        private void btnEnableService_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtComission.Text == null ||
                this.txtServiceID.Text == null)
            {
                MessageBox.Show("All field is required");
                return;
            }
            bool ser = int.TryParse(this.txtServiceID.Text, out int serviceID);
            if (!ser)
            {
                MessageBox.Show("Invalid serviceID");
                return;
            }
            var stylist = Application.Current.Properties["LoggedAccount"] as Account;
            bool result = stylistServiceService.EnableServiceStylist(stylist.AccountId, serviceID);
            if (result)
            {
                MessageBox.Show("Enable success");

            }
            else { MessageBox.Show("Already enable service."); }
            LoadGrid();
            resetInput();
        }

        private void resetInput()
        {
            this.txtServiceID.Text = null;
            this.txtComission.Text = null;
            this.ckbRate.IsChecked = false;
        }

        private void btnComission_Click(object sender, RoutedEventArgs e)
        {
            if (this.txtServiceID.Text.Trim() == null ||
              this.txtServiceID.Text.Trim() == null ||
              this.txtComission.Text.Trim() == null)
            {
                MessageBox.Show("All field is required");
                return;
            }
            string stylistID = this.tblStylistID.Text;
            int serviceID;
            if (!int.TryParse(this.txtServiceID.Text, out serviceID))
            {
                MessageBox.Show("Invalid ServiceID");
                return;
            }
            double commission;
            if (!double.TryParse(this.txtComission.Text, out commission))
            {
                MessageBox.Show("Invalid Commission");
                return;
            }
            bool result = stylistServiceService.UpdateComission(stylistID, serviceID, commission);
            if (result)
            {
                MessageBox.Show("Update success");
                LoadGridAll();
                resetInput();
            }
            else
            {
                MessageBox.Show("Cannot find service");
            }

        }
    }
}
