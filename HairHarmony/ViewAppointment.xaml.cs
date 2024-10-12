using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
using HairHarmony_BusinessObject;
using HairHarmony_Services;

namespace PRN212_HairHarmony
{
    /// <summary>
    /// Interaction logic for ViewAppointment.xaml
    /// </summary>
    public partial class ViewAppointment : Window
    {
        private readonly IAppointmentService appointmentService;
        private readonly IServiceService serviceService;
        private readonly IOrderService orderService;
        public ViewAppointment()
        {
            InitializeComponent();
            this.appointmentService = new AppointmentService();
            this.serviceService = new ServiceService();
            this.orderService = new OrderService();
            LoadGrid();
        }

        private void LoadGrid()
        {
            throw new NotImplementedException();
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

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            HomeManagerWindow HomeManagerWindow = new HomeManagerWindow();
            HomeManagerWindow.Show();
        }

        private void DataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            DataGridRow row = (DataGridRow)dataGrid.ItemContainerGenerator.ContainerFromIndex(dataGrid.SelectedIndex);

            DataGridCell RowColumn = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

            string appointmentid = ((TextBlock)RowColumn.Content).Text;
            Appointment appointment = appointmentService.GetById(Int32.Parse(appointmentid));

            txtAppointment.Text = appointmentid;
            txtDateTime.Text = appointment.AppointmentDate.ToString();



            // Lấy serviceName từ bảng Order và hiển thị tên dịch vụ
            Dictionary<int,List<string>> orders  = orderService.GetOrdersWithServiceNamesByAppointmentId(Int32.Parse(appointmentid));
            lbServiceName.ItemsSource = orders.Values;

            // Hiển thị tên stylist
            txtSytlistName.Text = appointment.StylistId.ToString();

            // Tính tổng tiền của tất cả các dịch vụ trong cuộc hẹn
            Dictionary<int,List<decimal?>> servicePrice = orderService.GetPriceWithServiceIDByAppointmentID(Int32.Parse(appointmentid));
            decimal? totalAmount = servicePrice.Values.SelectMany(list => list).Sum();
            txtTotal.Text = totalAmount.ToString("C"); // Định dạng tiền tệ
        }

    }
}
