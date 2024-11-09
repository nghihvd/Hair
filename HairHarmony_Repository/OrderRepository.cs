using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;
using HairHarmony_DAOs;

namespace HairHarmony_Repository
{
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrdersByAppointmentId(int appointmentId) => OrderDAO.Instance.DeleteOrdersByAppointmentId(appointmentId);
        

        public List<Order> GetAllOrders() => OrderDAO.Instance.GetAllOrders();  
        

        public Dictionary<int, List<(string serviceName,string stylistID)>> GetServiceNamesAndStylistByAppointmentId(int appointmentId) => OrderDAO.Instance.GetServiceNamesAndStylistByAppointmentId((int)appointmentId);

        public Dictionary<int, List<decimal?>> GetPriceWithServiceIDByAppointmentID(int appointmentId) => OrderDAO.Instance.GetPriceWithServiceIDByAppointmentID((int)appointmentId);

        public List<Order> GetOrderByStylistIDAndServiceID(string stylistId, int serviceID) => OrderDAO.Instance.GetOrderByStylistIDAndServiceID(stylistId, serviceID);


    }
}
