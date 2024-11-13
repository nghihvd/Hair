using HairHarmony_BusinessObject;
using HairHarmony_Services;
using System;
using System.Windows;

namespace PRN212_HairHarmony
{
    public partial class CustomerFeedbackWindow : Window
    {
        private readonly Order selectedOrder;
        private readonly IFeedbackService feedbackService;

        public CustomerFeedbackWindow(Order order)
        {
            InitializeComponent();
            selectedOrder = order;
            feedbackService = new FeedbackService();

            // Hiển thị thông tin Appointment ID, Service ID, và Stylist Name
            txtAppointmentID.Text = selectedOrder.AppointmentId.ToString();
            txtServiceID.Text = selectedOrder.ServiceId.ToString();
            txtStylistName.Text = selectedOrder.S.Stylist.Name;

            // Kiểm tra nếu feedback đã tồn tại
            var feedback = feedbackService.getFeedbackByAppoinIdAndServiceId(selectedOrder.AppointmentId, selectedOrder.ServiceId);

            if (feedback != null)
            {
                txtFeedback.Text = feedback.Comments;
                txtPoints.Text = feedback.Rating?.ToString() ?? "10";
                btnSubmit.IsEnabled = true;
            }
            else
            {
                txtPoints.Text = "10"; // Giá trị mặc định cho điểm
            }
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            string feedbackText = txtFeedback.Text;
            int points;
            if (!int.TryParse(txtPoints.Text, out points) || points < 1 || points > 10)
            {
                MessageBox.Show("Please enter a valid point between 1 and 10.", "Invalid Points", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (string.IsNullOrEmpty(feedbackText))
            {
                MessageBox.Show("Please provide feedback before submitting.", "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            SaveFeedback(selectedOrder.AppointmentId, selectedOrder.ServiceId, feedbackText, points, selectedOrder.StylistId);
            MessageBox.Show("Thank you for your feedback!", "Feedback Submitted", MessageBoxButton.OK, MessageBoxImage.Information);
            this.Close();
        }

        private void SaveFeedback(int appointmentId, int serviceId, string feedback, int points, string stylistId)
        {
            try
            {
                Feedback f = feedbackService.getFeedbackByAppoinIdAndServiceId(appointmentId, serviceId);
                if (f == null)
                {
                    AddLoyaltyPoints(points);
                    feedbackService.SaveFeedback(appointmentId, serviceId, feedback, points, stylistId);
                }
                else
                {
                    MessageBox.Show("Already feedback","Information",MessageBoxButton.OK, MessageBoxImage.Information);
                }
                

            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving feedback: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddLoyaltyPoints(int pointsToAdd)
        {
            Account loggedAccount = (Account)Application.Current.Properties["LoggedAccount"];

            if (loggedAccount != null)
            {
                AccountService accountService = new AccountService();
                loggedAccount.LoyaltyPoints = (loggedAccount.LoyaltyPoints ?? 0) + pointsToAdd;
                
                accountService.UpdateAccountLoyaltyPoints(loggedAccount);
                Application.Current.Properties["LoggedAccount"] = loggedAccount;
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
