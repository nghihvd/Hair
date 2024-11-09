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
    /// Interaction logic for BookServiceWindow.xaml
    /// </summary>
    public partial class BookServiceWindow : Window
    {
        private readonly IServiceService serviceService;
        public List<Service> SelectedServices;

        public BookServiceWindow()
        {
            InitializeComponent();
            serviceService = new ServiceService();       

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadGrid(); 
        }
        private void LoadGrid()
        {

            var serviceList = serviceService.GetServiceList();

            Console.WriteLine($"Kiểu của serviceList: {serviceList.GetType()}");
            foreach (var service in serviceList)
            {
                service.IsSelected = false; 
            }
            dtgBookService.ItemsSource = serviceList;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            
            var homeWindow = new HomeWindow();
            homeWindow.Show();
            this.Close();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnBookSerNext_Click(object sender, RoutedEventArgs e)
        {
            foreach (var service in dtgBookService.ItemsSource.Cast<Service>())
            {
                Console.WriteLine($"Service: {service.ServiceName}, IsSelected: {service.IsSelected}");
            }

            var services = (List<Service>)dtgBookService.ItemsSource;
            SelectedServices = services.Where(s => s.IsSelected ?? false).ToList();
            lstSelectedServices.Items.Clear();
            foreach (var service in SelectedServices)
            {
                lstSelectedServices.Items.Add($"{service.ServiceName} - ${service.Price} - {service.Duration} mins");
            }
            if (SelectedServices.Count == 0)
            {
                MessageBox.Show("Please select at least one service.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            MessageBox.Show("Services selected successfully! Proceeding to the next step.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            BookDateWindow bookDateWindow = new BookDateWindow(SelectedServices);
            bookDateWindow.Show();
            this.Close();
        }

        private void dtgBookService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedService = (Service)dtgBookService.SelectedItem;

            if (selectedService != null)
            {
                txtServiceName.Text = selectedService.ServiceName;
                txtServicePrice.Text = selectedService.Price.HasValue ? $"{selectedService.Price:C2}" : "N/A"; 
                txtServiceDuration.Text = selectedService.Duration.HasValue ? selectedService.Duration.ToString() : "N/A";

            }
        }
    }
}
