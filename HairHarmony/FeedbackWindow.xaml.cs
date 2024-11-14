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
        private readonly IAppointmentService appointmentService;
        private readonly IServiceService serviceService;
        public FeedbackWindow()
        {
            InitializeComponent();
            this.feedbackService = new FeedbackService();
            this.appointmentService = new AppointmentService();
            this.serviceService = new ServiceService();
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
            HomeStylistWindow stylistHomeWindow = new HomeStylistWindow();
            stylistHomeWindow.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {


            DataGrid dataGrid = sender as DataGrid;
           
            DataGridRow row =
                (DataGridRow)dataGrid.ItemContainerGenerator
                .ContainerFromIndex(dataGrid.SelectedIndex);

            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

            string id = ((TextBlock)RowColumn.Content).Text;
            if (id.Trim().Length ==0) return;
            int feedbackID = int.Parse(id);

            Feedback feedback = feedbackService.searchFeedback(feedbackID);
            txtFeedback.Text = feedback.Comments;
            txtAppointmentID.Text = feedback.AppointmentId.ToString();
            pgbPoints.Value = (double)feedback.Rating;
            tblPoint.Text = pgbPoints.Value.ToString();
            Service service = serviceService.GetServiceByID(feedback.ServiceId);
            txtServiceID.Text =service.ServiceName;
            tblDate.Text = appointmentService.GetById(feedback.AppointmentId).AppointmentDate.ToString();
        }
        private void LoadGrid()
        {

            var log = Application.Current.Properties["LoggedAccount"] as Account; // stylist acc

            this.dtgName.ItemsSource = feedbackService.getFeedbackByStylistID(log.AccountId);

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }

    }
}
