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

namespace PRN212_HairHarmony
{
    /// <summary>
    /// Interaction logic for BookViewWindow.xaml
    /// </summary>
    public partial class BookViewWindow : Window
    {
        private List<Service> selectedServices;
        private DateTime selectedDateTime;
        private Account currentAccount;

        public BookViewWindow(List<Service> selectedServices, DateTime selectedDateTime)
        {
            InitializeComponent();
            this.selectedServices = selectedServices;
            this.selectedDateTime = selectedDateTime;
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
    }
}
