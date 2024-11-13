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
    /// Interaction logic for StylistAppointmentView.xaml
    /// </summary>
    public partial class StylistAppointmentView : Window
    {
        private readonly IAppointmentService appointmentService;
        private readonly IServiceService serviceService;
        private readonly IOrderService orderService;
        private readonly IAccountService accountService;
        private readonly IStylistServiceService stylistServiceService;
        private Account? account;
        private decimal totalAmount;


        public StylistAppointmentView(Account account)
        {
            InitializeComponent();
            appointmentService = new AppointmentService();
            serviceService = new ServiceService();
            orderService = new OrderService();
            accountService = new AccountService();
            stylistServiceService = new StylistServiceService();
            LoadData();
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

        private void LoadData()
        {
            var account = Application.Current.Properties["LoggedAccount"] as Account;
            if (account != null)
            {
                this.dtgAppointment.ItemsSource = appointmentService.GetAll().Select(a => new { a.AppointmentId, a.AppointmentDate, a.CustomerId, a.Status });

            }

        }

        private void dtgAppoitment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedIndex >= 0)
            {
                var row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                var column0 = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
                if (column0 != null)
                {

                    string appointmentid = ((TextBlock)column0.Content).Text;
                    Appointment appointment = appointmentService.GetById(Int32.Parse(appointmentid));
                    txtDateTime.Text = appointment.AppointmentDate.ToString();
                    Dictionary<int, List<(string? ServiceName, decimal? Price, int? Duration)>> orders = orderService.GetServiceDetailsByAppointmentID(Int32.Parse(appointmentid));
                    lbService.ItemsSource = orders.Values.SelectMany(list => list).ToList();
                    Dictionary<int, List<decimal?>> servicePrice = orderService.GetPriceWithServiceIDByAppointmentID(Int32.Parse(appointmentid));
                    decimal? totalAmount = servicePrice.Values.SelectMany(list => list).Sum();
                    txtTotal.Text = totalAmount?.ToString("C") ?? "N/A";
                }

                var column2 = dataGrid.Columns[2].GetCellContent(row)?.Parent as DataGridCell;
                if (column2 != null)
                {
                    string customerID = ((TextBlock)column2.Content).Text;
                    var accountinfo = accountService.getAccountByID(customerID);

                    if (accountinfo != null)
                    {
                        txtCustomerName.Text = accountinfo.Name;
                    }
                }
            }
        }

        private void btnFinish_Click(object sender, RoutedEventArgs e)
        {
            var account = Application.Current.Properties["LoggedAccount"] as Account;

            if (account != null)
            {
                var stylistInfo = stylistServiceService.GetStylisServiceByStylistId(account.AccountId);
                if (stylistInfo != null && stylistInfo.CommissionRate.HasValue)
                {
                    decimal? oldSalary = account.Salary ?? 0;
                    decimal commissionAmount = totalAmount * (decimal)(stylistInfo.CommissionRate.Value / 100.0); // Convert percentage to decimal
                    decimal newSalary = oldSalary.Value + commissionAmount;


                    account.Salary = newSalary;
                    accountService.UpdateAccount(account);

                }
            }

        }
    }
}

