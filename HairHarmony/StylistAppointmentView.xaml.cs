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
using System.Windows.Ink;
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
                // Lấy danh sách appointmentID từ stylist
                var appointmentIds = orderService.GetAppointmentsByStylistId(account.AccountId);

                // Lấy chi tiết từng appointment dựa trên appointmentID
                var appointments = appointmentIds
                    .Select(id => appointmentService.GetById(id))
                    .Where(a => a != null)
                    .Select(a => new
                    {
                        AppointmentID = a.AppointmentId,
                        CustomerID = a.CustomerId,
                        AppointmentDate = a.AppointmentDate,
                        Status = a.Status
                    })
                    .ToList();

                // Gán dữ liệu vào DataGrid
                this.dtgAppointment.ItemsSource = appointments;
            }
        }


        private void dtgAppoitment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //if (string.IsNullOrEmpty(lbService.SelectedValue.ToString()) || lbService.SelectedValue == null)
            //{
            //    return;
            //}

            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.SelectedIndex >= 0)
            {
                var row = dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;

                var column0 = dataGrid.Columns[0].GetCellContent(row)?.Parent as DataGridCell;
                if (column0 != null)
                {

                    string appointmentid = ((TextBlock)column0.Content).Text;
                    Appointment appointment = appointmentService.GetById(Int32.Parse(appointmentid));
                    if(appointment == null) return;
                    txtDateTime.Text = appointment.AppointmentDate.ToString();
                    Dictionary<int, List<(int ServiceID,string? ServiceName, decimal? Price, int? Duration)>> orders = orderService.GetServiceDetailsByAppointmentID(Int32.Parse(appointmentid));
                    if(orders == null)
                    {
                        return ;
                    }
                    lbService.ItemsSource = orders.Values.SelectMany(list => list).ToList();
                    Dictionary<int, List<decimal?>> servicePrice = orderService.GetPriceWithServiceIDByAppointmentID(Int32.Parse(appointmentid));
                    decimal? totalAmount = servicePrice.Values.SelectMany(list => list).Sum();
                    txtTotal.Text = totalAmount?.ToString("C") ?? "N/A";
                }

                var column2 = dataGrid.Columns[1].GetCellContent(row)?.Parent as DataGridCell;
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
            try
            {
                var account = Application.Current.Properties["LoggedAccount"] as Account;
                if (account != null)
                {
                    var serviceIds = stylistServiceService.GetServiceIdsByStylistId(account.AccountId);
                    decimal totalCommission = 0;
                    decimal? oldSalary = account.Salary ?? 0;

                    if (serviceIds == null || !serviceIds.Any())
                    {
                        MessageBox.Show("No services found for this stylist");
                        return;
                    }

                    foreach (var serviceId in serviceIds)
                    {
                        var stylistInfo = stylistServiceService.GetStylistServiceByStylistIDAndServiceID(account.AccountId, serviceId);
                        if (stylistInfo == null)
                        {
                            MessageBox.Show($"Not found information of stylist service for service ID: {serviceId}");
                            continue;
                        }

                        if (stylistInfo.CommissionRate.HasValue)
                        {
                            decimal commissionRate = (decimal)stylistInfo.CommissionRate.Value;
                            var appointments = orderService.GetAppointmentsByStylistId(account.AccountId);

                            if (appointments != null && appointments.Any())
                            {
                                foreach (var appointmentId in appointments)
                                {
                                    var services = orderService.GetServiceDetailsByAppointmentID(appointmentId);
                                    foreach (var serviceDetails in services.Values.SelectMany(s => s))
                                    {
                                        if (serviceDetails.ServiceId == serviceId )
                                        {
                                            bool re = orderService.Update( stylistInfo.StylistId, stylistInfo.ServiceId,appointmentId);
                                            if (re)
                                            {
                                                decimal price = serviceDetails.Price ?? 0;
                                                totalCommission += price * commissionRate;
                                                
                                            }
                                            else
                                            {
                                                MessageBox.Show("Already add salary");
                                                
                                            }

                                        }
                                    }
                                    appointmentService.UpdateStatus(appointmentId,"Completed");

                                }
                            }
                        }
                    }
                    
                    decimal newSalary = oldSalary.Value + totalCommission;
                    account.Salary = newSalary;
                    accountService.UpdateAccount(account);

                    MessageBox.Show($"Salary update successful!\nOld Salary: {oldSalary:C}\nCommission: {totalCommission:C}\nNew Salary: {newSalary:C}",
                                  "Success",
                                  MessageBoxButton.OK,
                                  MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("No logged account found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}

