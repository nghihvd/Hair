using HairHarmony_BusinessObject;
using HairHarmony_Services;
using System;
using System.Windows;

namespace PRN212_HairHarmony
{
    public partial class CustomerFeedbackWindow : Window
    {
        private int appointmentId;
        private readonly IFeedbackService feedbackService;

        public CustomerFeedbackWindow(int appointmentId)
        {
            InitializeComponent();
            feedbackService = new FeedbackService();
            this.appointmentId = appointmentId;

            txtAppointmentID.Text = appointmentId.ToString();

            Account loggedAccount = (Account)Application.Current.Properties["LoggedAccount"];
            var feedback = feedbackService.getFeedbackByAppoinId(appointmentId);

            if (feedback != null)
            {
                txtFeedback.Text = feedback.Comments;
                txtPoints.Text = feedback.Rating.ToString();
            }
            else
            {
                txtPoints.Text = "10";
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string feedback = txtFeedback.Text;
            int points;

            if (!int.TryParse(txtPoints.Text, out points) || points < 1 || points > 10)
            {
                MessageBox.Show("Please enter a valid point between 1 and 10.", "Invalid Points", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            if (string.IsNullOrEmpty(feedback))
            {
                MessageBox.Show("Please provide feedback before submitting.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            SaveFeedback(appointmentId, feedback, points);

            MessageBox.Show("Thank you for your feedback!", "Feedback Submitted", MessageBoxButton.OK, MessageBoxImage.Information);
            AppointmentWindow ap = new AppointmentWindow();
            ap.Show();
            this.Close();
        }

        private void SaveFeedback(int appointmentId, string feedback, int points)
        {
            Account loggedAccount = (Account)Application.Current.Properties["LoggedAccount"];
            feedbackService.SaveFeedback(appointmentId, feedback, points, loggedAccount.AccountId + "");
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            AppointmentWindow ap = new AppointmentWindow();
            ap.Show();
            this.Close();
        }
    }
}
