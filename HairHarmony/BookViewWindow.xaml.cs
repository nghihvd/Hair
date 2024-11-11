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
    /// Interaction logic for BookViewWindow.xaml
    /// </summary>
    public partial class BookViewWindow : Window
    {
        private readonly IShiftService shiftService;
        private readonly IAppointmentService appointmentService;
        private readonly IOrderService orderService;

        private List<Service> selectedServices;
        private DateTime selectedDateTime;
        private Account currentAccount;
        Appointment currentAppointment;
        List<Shift> allShiftCreated;

        public BookViewWindow(List<Service> selectedServices, DateTime selectedDateTime, Appointment currentAppointment,List<Shift> allShiftCreated)
        {
            InitializeComponent();
            shiftService = new ShiftService();
            appointmentService = new AppointmentService();
            orderService = new OrderService();

            this.selectedServices = selectedServices;
            this.selectedDateTime = selectedDateTime;
            this.currentAppointment= currentAppointment;
            this.allShiftCreated = allShiftCreated;
            currentAccount = (Account)Application.Current.Properties["LoggedAccount"];

            txtCusName.Text = currentAccount.Name;

            txtDateTime.Text = selectedDateTime.ToString("MM/dd/yyyy HH:mm");

            foreach (var service in selectedServices)
            {
                lstSelectedServices.Items.Add($"{service.ServiceName} - ${service.Price} - {service.Duration} mins");
            }

            decimal totalPrice = selectedServices.Sum(service => service.Price ?? 0);
            txtTotalPrice.Text = $"${totalPrice}";

        }


        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBookViewNext_Click(object sender, RoutedEventArgs e)
        {
            BookSuccessfulWindow bookSuccessfulWindow = new BookSuccessfulWindow();
            bookSuccessfulWindow.Show();
            this.Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            if (allShiftCreated.Count > 0)
            {
                shiftService.DeleteAllShifts(allShiftCreated);
            }
            orderService.DeleteOrdersByAppointmentId(currentAppointment.AppointmentId);
            appointmentService.RemoveByID(currentAppointment.AppointmentId);

            MessageBox.Show("Cancel booking successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            var homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }
    }
}
