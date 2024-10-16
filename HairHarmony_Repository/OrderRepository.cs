﻿using System;
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
        

        public Dictionary<int, List<string>> GetOrdersWithServiceNamesByAppointmentId(int appointmentId) => OrderDAO.Instance.GetOrdersWithServiceNamesByAppointmentId((int)appointmentId);

        public Dictionary<int, List<decimal?>> GetPriceWithServiceIDByAppointmentID(int appointmentId) => OrderDAO.Instance.GetPriceWithServiceIDByAppointmentID((int)appointmentId);
        
    }
}
