using HairHarmony_BusinessObject;
using HairHarmony_Repository;
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
    /// Interaction logic for FeedbackWindow.xaml
    /// </summary>
    public partial class FeedbackWindow : Window
    {
        private readonly IFeedbackService feedbackService;
        public FeedbackWindow()
        {
            InitializeComponent();
            this.feedbackService = new FeedbackService();
            LoadGrid(); 
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            StylistHomeWindow stylistHomeWindow = new StylistHomeWindow();  
            stylistHomeWindow.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void LoadGrid()
        {
            var log = Application.Current.Properties["LoggedAccount"] as Account;
            this.dtgName.ItemsSource = feedbackService.getFeedbackById(log.AccountId).Select(a => new {a.FeedbackId });

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
