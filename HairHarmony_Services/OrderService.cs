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

        public Dictionary<int, List<string>> GetOrdersWithServiceNamesByAppointmentId(int appointmentId)
        {
            return orderrepo.GetOrdersWithServiceNamesByAppointmentId(appointmentId);
        }
    }
}
