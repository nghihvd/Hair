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
    /// Interaction logic for HomeWindow.xaml
    /// </summary>
    public partial class HomeWindow : Window
    {
        private Account? account;
        public HomeWindow()
        {
            InitializeComponent();
           account = Application.Current.Properties["LoggedAccount"] as Account;
            this.tblWelcome.Text = "Welcome "+ account.Name;
        }

        private void btnService_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ServiceWindow serviceWindow = new ServiceWindow();
            serviceWindow.Show();

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }




        private void btnAppointment_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            AppointmentWindow appoitmentWindow = new AppointmentWindow();
            appoitmentWindow.Show();
        }


        private void btnStylist_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.Show();
            this.Close();
        }

        private void btnBookService_Click(object sender, RoutedEventArgs e)
        {
            BookServiceWindow bookServiceWindow = new BookServiceWindow();
            bookServiceWindow.Show();
            this.Close();
        }
    }
}
