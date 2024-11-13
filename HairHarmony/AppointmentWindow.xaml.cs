using HairHarmony_BusinessObject;
using HairHarmony_DAOs;
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
    /// Interaction logic for AppointmentWindow.xaml
    /// </summary>
    public partial class AppointmentWindow : Window
    {
        private readonly IAppointmentService appoitmentService;
        private List<Appointment> Appointments;
        public AppointmentWindow()
        {
            InitializeComponent();
            appoitmentService = new AppointmentService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            Account loggedAccount = (Account)Application.Current.Properties["LoggedAccount"];
            AppointmentDAO appointmentDAO = new AppointmentDAO();
            this.dtgAppointment.ItemsSource = appointmentDAO.GetAppointmentsByUserId(loggedAccount.AccountId).Where(a => !a.Status.Equals("Cancelled"));
        }

        private Order? selectedOrder;

        private void lstServices_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstServices.SelectedItem is string selectedServiceName && dtgAppointment.SelectedItem is Appointment selectedAppointment)
            {
                selectedOrder = selectedAppointment.Orders
                    .FirstOrDefault(o => o.S.Service.ServiceName == selectedServiceName);
            }
        }




        private void ResetInput()
        {

        }
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {


        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
            ResetInput();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeWindow homeWindow = new HomeWindow();
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
            if (dtgAppointment.SelectedItem is Appointment selectedAppointment)
            {
                DisplayAppointmentDetails(selectedAppointment);
                btnFeedback.Visibility = selectedAppointment.Status == "Completed"
            ? Visibility.Visible
            : Visibility.Collapsed;
            }
        }

        private void DisplayAppointmentDetails(Appointment appointment)
        {
            txtAppointmentID.Text = appointment.AppointmentId.ToString();
            txtAppointmentDate.Text = appointment.AppointmentDate?.ToString("MM/dd/yyyy") ?? "Unknown";
            txtTotalPrice.Text = appointment.Orders.Sum(o => o.Price ?? 0).ToString("C");

            // Hiển thị tên stylist cho dịch vụ đầu tiên mặc định
            var firstOrder = appointment.Orders.FirstOrDefault();
            txtStylistName.Text = firstOrder?.S.Stylist.Name ?? "Unknown";

            // Hiển thị danh sách các dịch vụ
            lstServices.ItemsSource = appointment.Orders.Select(o => o.S.Service.ServiceName).ToList();
        }







        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var selectedAppointment = (Appointment)dtgAppointment.SelectedItem;
            if (selectedAppointment != null)
            {
                selectedAppointment.Status = "Cancelled";
                appoitmentService.UpdateStatus(selectedAppointment.AppointmentId, selectedAppointment.Status);

                MessageBox.Show("Appointment has been cancelled.", "Cancelled", MessageBoxButton.OK, MessageBoxImage.Information);

                LoadGrid();
                ResetInput();
            }
        }

        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {
            if (selectedOrder != null)
            {
                CustomerFeedbackWindow feedbackWindow = new CustomerFeedbackWindow(selectedOrder);
                feedbackWindow.ShowDialog();
                selectedOrder = null;
            }
            else
            {
                MessageBox.Show("Please select a service to provide feedback.");
            }
        }




    }
}
