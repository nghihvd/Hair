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
    /// Interaction logic for HomeStylistWindow.xaml
    /// </summary>
    public partial class HomeStylistWindow : Window
    {
        private Account? account;
        public HomeStylistWindow()
        {
            InitializeComponent();
            account = Application.Current.Properties["LoggedAccount"] as Account;
        }
        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }


        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FeedbackWindow feedbackWindow = new FeedbackWindow();
            feedbackWindow.Show();
        }

        private void btnViewComission_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            ViewComissionRate viewComissionRate = new ViewComissionRate();
            viewComissionRate.Show();
        }




        private void btnAppoinment_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            StylistAppointmentView stylistAppointmentView = new StylistAppointmentView(account);
            stylistAppointmentView.Show();
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            StylistAccountInformation stylistAccountInformation = new StylistAccountInformation(account);
            stylistAccountInformation.Show();
        }
    }
}
