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
    /// Interaction logic for BookStylistWindow.xaml
    /// </summary>
    public partial class BookStylistWindow : Window
    {
        private readonly IShiftService shiftService;
        private readonly IStylistServiceService stylistServiceService;
        private readonly IAppointmentService appointmentService;
        private readonly IOrderService orderService;

        private List<Service> selectedServices;
        private DateTime selectedDateTime;
        private Appointment currentAppointment;
        private Account currentAccount;

        private int currentServiceIndex = 0;

        public BookStylistWindow(List<Service> selectedServices, DateTime selectedDateTime)
        {
            InitializeComponent();
            this.selectedServices = selectedServices;
            this.selectedDateTime = selectedDateTime;

            shiftService = new ShiftService();
            appointmentService = new AppointmentService();
            orderService = new OrderService();
            stylistServiceService = new StylistServiceService();
            currentAccount = (Account)Application.Current.Properties["LoggedAccount"];

            currentAppointment = appointmentService.CreateNewAppointment(selectedDateTime, currentAccount.AccountId);

            DisplaySelectedServices();
            LoadAvailableStylists();
            DisplaySelectedDateTime();
            DisplayCurrentService();

        }


        private void DisplaySelectedServices()
        {
            foreach (var service in selectedServices)
            {
                Console.WriteLine($"Service: {service.ServiceName}");
            }
            foreach (var service in selectedServices)
            {
                lstSelectedServices.Items.Add($"{service.ServiceName} - ${service.Price} - {service.Duration} mins");
            }
        }
        private void LoadAvailableStylists()
        {
            var stylistList = new AccountService().GetStylists();
            dtgBookStylist.ItemsSource = stylistList;
        }

        private void DisplaySelectedDateTime()
        {
            tblDate.Text = $"Selected Date & Time: {selectedDateTime:MM/dd/yyyy HH:mm}";
        }

        private void DisplayCurrentService()
        {
            if (currentServiceIndex < selectedServices.Count)
            {
                txtShowService.Text = selectedServices[currentServiceIndex].ServiceName;
            } else
            {
                txtShowService.Text = "None";
            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBookSerNext_Click(object sender, RoutedEventArgs e)
        {
            if (selectedServices.Count == currentServiceIndex)
            {
                BookViewWindow bookViewWindow = new BookViewWindow(selectedServices, selectedDateTime);
                bookViewWindow.Show();
                this.Close();
            } else
            {
                MessageBox.Show("Please select a stylist to create an order.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
                
        }

        private void btnCreateOrder_Click(object sender, RoutedEventArgs e)
        {
            if (dtgBookStylist.SelectedItem is Account selectedStylist)
            {
                var selectedService = selectedServices[currentServiceIndex];
                var getAllShift = shiftService.GetAllShifts();
                bool checkFreeStylist = true;
                var newOrder = new Order
                {
                    ServiceId = selectedService.ServiceId,
                    AppointmentId = currentAppointment.AppointmentId,
                    StylistId = selectedStylist.AccountId,
                    Price = selectedService.Price
                };

                foreach(Shift aShift in getAllShift)
                {
                    if (aShift.StylistId == selectedStylist.AccountId)
                    {
                        if(selectedDateTime.TimeOfDay >= aShift.StartTime && selectedDateTime.TimeOfDay <= aShift.EndTime && selectedDateTime == aShift.Date)
                        {
                            checkFreeStylist = false;
                        }
                    }
                }
                if (checkFreeStylist)
                {
                    orderService.CreateOrder(newOrder);
                    MessageBox.Show("Order created successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                    var newShift = new Shift
                    {
                        StylistId = selectedStylist.AccountId,
                        StartTime = selectedDateTime.TimeOfDay,
                        EndTime = selectedDateTime.TimeOfDay.Add(TimeSpan.FromMinutes((long)selectedService.Duration)),
                        Date = selectedDateTime.Date
                    };

                    selectedDateTime = selectedDateTime.Add(TimeSpan.FromMinutes((long)selectedService.Duration));

                    shiftService.AddShift(newShift);
                    MessageBox.Show("Shift created for the stylist.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                    currentServiceIndex += 1;
                    if (selectedServices.Count != currentServiceIndex)
                    {
                        DisplayCurrentService();  
                    }
                    else
                    {
                        DisplayCurrentService();
                        MessageBox.Show("All services have been assigned a stylist.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                } else
                {
                    MessageBox.Show("This stylist is not available during the selected time period.", "Notification", MessageBoxButton.OK, MessageBoxImage.Information);

                }

            }
            else
            {
                MessageBox.Show("Please select a stylist before creating an order.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void dtgBookStylist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dtgBookStylist.SelectedItem is Account selectedStylist)
            {
                txtStylistName.Text = selectedStylist.Name;
                txtStylistPoint.Text = selectedStylist.LoyaltyPoints.ToString();
                txtStylistPhoneNum.Text = selectedStylist.Phone;
            }
        }
    }
}
