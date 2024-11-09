using HairHarmony_BusinessObject;
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
using HairHarmony_BusinessObject;

namespace PRN212_HairHarmony
{
    /// <summary>
    /// Interaction logic for BookDateWindow.xaml
    /// </summary>
    public partial class BookDateWindow : Window
    {
        private List<Service> selectedServices;
        public DateTime SelectedDateTime { get; set; }

        public BookDateWindow(List<Service> services)
        {
            InitializeComponent();
            selectedServices = services; 
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            for (int hour = 7; hour <= 20; hour++)
            {
                hourComboBox.Items.Add(hour.ToString("D2"));
            }

            for (int minute = 0; minute < 60; minute++)
            {
                minuteComboBox.Items.Add(minute.ToString("D2")); 
            }

            hourComboBox.SelectedIndex = DateTime.Now.Hour;
            minuteComboBox.SelectedIndex = DateTime.Now.Minute / 15;

            LoadSelectedServices();
        }

        private void LoadSelectedServices()
        {

            foreach (var service in selectedServices)
            {
                Console.WriteLine($"Selected Service: {service.ServiceName} - Price: {service.Price} - Duration: {service.Duration}");

            }
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            BookServiceWindow bookServiceWindow = new BookServiceWindow();
            bookServiceWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();

        }

        private void btnBookSerNext_Click(object sender, RoutedEventArgs e)
        {
            var selectedDate = datePickBookSer.SelectedDate;
            DateTime currentDateTime = DateTime.Now;

            if (!selectedDate.HasValue)
            {
                MessageBox.Show("Please select a date.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int hour = int.Parse(hourComboBox.SelectedItem.ToString());
            int minute = int.Parse(minuteComboBox.SelectedItem.ToString());

            SelectedDateTime = selectedDate.Value.Date.AddHours(hour).AddMinutes(minute);

            if (SelectedDateTime < currentDateTime)
            {
                MessageBox.Show("Selected date and time cannot be in the past. Please select a future date and time.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (SelectedDateTime > currentDateTime.AddDays(3))
            {
                MessageBox.Show("Selected date and time cannot be more than 3 days from today. Please select a closer date.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show($"Selected Date and Time: {SelectedDateTime}", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);

            BookStylistWindow bookStylistWindow = new BookStylistWindow(selectedServices, SelectedDateTime);
            bookStylistWindow.Show();
            this.Close();


        }



    }
}
