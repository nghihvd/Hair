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
        public FeedbackWindow()
        {
            InitializeComponent();
            this.feedbackService = new FeedbackService();
            this.appointmentService = new AppointmentService();
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
            int appointID = int.Parse(id);
            Feedback feedback = feedbackService.getFeedbackByAppoinId(appointID);
            txtFeedback.Text = feedback.Comments;
            txtAppointmentID.Text = feedback.AppointmentId.ToString();
            pgbPoints.Value = (double)feedback.Rating;
            tblPoint.Text = pgbPoints.Value.ToString();

        }
        private void LoadGrid()
        {

            var log = Application.Current.Properties["LoggedAccount"] as Account; // stylist acc
            List<Appointment> appoint = appointmentService.getAppointmentByStylistID(log.AccountId);
            List<Feedback> feedbacks = new List<Feedback>();
            foreach(Appointment ap in appoint)
            {
                Feedback f = feedbackService.getFeedbackByAppoinId(ap.AppointmentId);
                if ( f != null)
                {
                    feedbacks.Add(f);
                }
            }
            this.dtgName.ItemsSource = feedbacks.Select(a => new {a.AppointmentId });

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
