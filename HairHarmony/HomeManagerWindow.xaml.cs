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
    /// Interaction logic for HomeManagerWindow.xaml
    /// </summary>
    public partial class HomeManagerWindow : Window
    {

        
        public HomeManagerWindow()
        {
            InitializeComponent();
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


        private void btnViewStylist_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ViewStylistWindow stylistWindow = new ViewStylistWindow();
            stylistWindow.Show();
        }

        private void btnViewService_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ServiceWindow serviceWindow = new ServiceWindow(1);
            serviceWindow.Show();
        }

        private void btnViewAppoint_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ViewAppointment appointWindow = new ViewAppointment();
            appointWindow.Show();
        }
    }
}
