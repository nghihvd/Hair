using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;
using HairHarmony_Repository;

namespace HairHarmony_Services
{
    public class OrderService : IOrderService
    {
        private OrderRepository orderrepo;

        public OrderService()
        {
            orderrepo = new OrderRepository();
        }
        public void DeleteOrdersByAppointmentId(int appointmentId)
        {
            orderrepo.DeleteOrdersByAppointmentId(appointmentId);
        }

        public List<Order> GetAllOrders()
        {
            return orderrepo.GetAllOrders();
        }

        public Dictionary<int, List<(string serviceName,string stylistID)>> GetServiceNamesAndStylistByAppointmentId(int appointmentId)
        {
            return orderrepo.GetServiceNamesAndStylistByAppointmentId(appointmentId);
        }

        public Dictionary<int, List<decimal?>> GetPriceWithServiceIDByAppointmentID(int appointmentId)
        {
            return orderrepo.GetPriceWithServiceIDByAppointmentID(appointmentId);
        }

        public List<Order> GetOrderByStylistIDAndServiceID(string stylistId, int serviceID)
        {
            return orderrepo.GetOrderByStylistIDAndServiceID(stylistId, serviceID);
        }
    }
}
