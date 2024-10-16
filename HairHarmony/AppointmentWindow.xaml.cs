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
        public AppointmentWindow()
        {
            InitializeComponent();
            appoitmentService  = new AppointmentService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            Account loggedAccount = (Account)Application.Current.Properties["LoggedAccount"];
            this.dtgAppointment.ItemsSource = appoitmentService.getAppointmentByCustomerID(loggedAccount.AccountId);
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
            var selectedAppointment = (Appointment)dtgAppointment.SelectedItem;

            if (selectedAppointment != null)
            {
                txtAppointmentID.Text = selectedAppointment.AppointmentId.ToString();
                txtStylistName.Text = selectedAppointment.Stylist?.Name ?? "No Stylist";
                txtAppointmentDate.Text = selectedAppointment.AppointmentDate?.ToString("MM/dd/yyyy HH:mm") ?? "No Date";

                lstServices.Items.Clear();

                var servicesWithPrices = OrderDAO.Instance.GetServicesWithPricesByAppointmentId(selectedAppointment.AppointmentId);

                if (servicesWithPrices != null && servicesWithPrices.ContainsKey(selectedAppointment.AppointmentId))
                {
                    var serviceList = servicesWithPrices[selectedAppointment.AppointmentId];

                    decimal totalPrice = 0;

                    foreach (var service in serviceList)
                    {
                        lstServices.Items.Add($"{service.ServiceName} - {service.Price:C2}");

                        totalPrice += service.Price;
                    }

                    txtTotalPrice.Text = $"Total: {totalPrice:C2}";
                }
                if (selectedAppointment.Status != "Finished") 
                {
                    btnFeedback.IsEnabled = false; 
                    btnCancel.Visibility = Visibility.Visible; 
                }
                else
                {
                    btnFeedback.IsEnabled = true; 
                    btnCancel.Visibility = Visibility.Collapsed; 
                }
            }
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
            var selectedAppointment = (Appointment)dtgAppointment.SelectedItem;
            CustomerFeedbackWindow fb = new CustomerFeedbackWindow(selectedAppointment.AppointmentId);
            fb.Show();
            this.Close();
        }



    }
}
