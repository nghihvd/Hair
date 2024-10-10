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
        public HomeStylistWindow()
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
            this.Close();
        }


        private void btnFeedback_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            FeedbackWindow feedbackWindow = new FeedbackWindow();
            feedbackWindow.Show();
        }
    }
}
