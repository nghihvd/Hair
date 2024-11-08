using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;

namespace HairHarmony_Repository
{
    public interface IOrderRepository
    {
        public List<Order> GetAllOrders();

        public Dictionary<int, List<(string serviceName,string stylistID)>> GetServiceNamesAndStylistByAppointmentId(int appointmentId);

        public void DeleteOrdersByAppointmentId(int appointmentId);

        public Dictionary<int, List<decimal?>> GetPriceWithServiceIDByAppointmentID(int appointmentId);


    }
}
