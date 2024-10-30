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
    /// Interaction logic for ViewComissionRate.xaml
    /// </summary>
    public partial class ViewComissionRate : Window
    {
        private  readonly IStylistServiceService stylistServiceService;
        public ViewComissionRate()
        {
            InitializeComponent();
            stylistServiceService = new StylistServiceService();    
            LoadGrid(); 
        }

        
        public void LoadGrid()
        {
            this.dtgService.ItemsSource = stylistServiceService.getListServiceStylist();
        }
        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row =
                (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);

            DataGridCell cell = 
                dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

            string id = ((TextBlock)cell.Content).Text; // cột đầu tiên là cột stylistID
            StylistService stylistService = stylistServiceService.GetStylistServiceByStylistID(id);
            this.txtServiceID.Text = stylistService.ServiceId.ToString();
            this.txtComission.Text =  stylistService.CommissionRate.ToString();
            this.ckbRate.IsChecked = stylistService.Status;
                

        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeStylistWindow homeStylistWindow = new HomeStylistWindow();
            homeStylistWindow.Show();
        }

        private void btnLogout_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
        }

        private void btnReload_Click(object sender, RoutedEventArgs e)
        {
            LoadGrid();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
