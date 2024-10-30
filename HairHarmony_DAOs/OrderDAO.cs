using System;
using System.Collections.Generic;
using System.Linq;
using HairHarmony_BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace HairHarmony_DAOs
{
    public class OrderDAO
    {
        private HairContext dbContext;
        private static OrderDAO instance = null;
        public static OrderDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new OrderDAO();
                }
                return instance;
            }
        }

        public OrderDAO()
        {
            dbContext = new HairContext();
        }

       

        // Get all orders
        public List<Order> GetAllOrders()
        {
            return dbContext.Orders.ToList();
        }

        public Dictionary<int, List<string>> GetOrdersWithServiceNamesByAppointmentId(int appointmentId)
        {
            var ordersWithServices = dbContext.Orders
                .Where(o => o.AppointmentId == appointmentId)
                .Join(dbContext.Services,
                    order => order.ServiceId,
                    service => service.ServiceId,
                    (order, service) => new { order.AppointmentId, ServiceName = service.ServiceName })
                .ToList();

            if (ordersWithServices != null && ordersWithServices.Any())
            {
                var result = new Dictionary<int, List<string>>();
                result[appointmentId] = ordersWithServices.Select(o => o.ServiceName).ToList();
                return result;
            }
            return null;
        }


        public void DeleteOrdersByAppointmentId(int appointmentId)
        {
            var orders = dbContext.Orders.Where(o => o.AppointmentId == appointmentId).ToList();
            if (orders != null && orders.Any())
            {
                dbContext.Orders.RemoveRange(orders);
                dbContext.SaveChanges();
            }
        }

        public Dictionary<int,List<decimal?>> GetPriceWithServiceIDByAppointmentID(int appointmentId) 
        {
            var orderWithService = dbContext.Orders
                .Where(o => o.AppointmentId == appointmentId)
                .Join(dbContext.Services,order => order.ServiceId,service => service.ServiceId,(order,service) => new { order.AppointmentId, servicePrice = service.Price }).ToList();

            if (orderWithService != null && orderWithService.Any()) 
            {
                var result = new Dictionary <int, List<decimal?>>();
                result[appointmentId] = orderWithService.Select(o => o.servicePrice).ToList();
                return result;
            }
            return null;
        }

        public Dictionary<int, List<(string ServiceName, decimal Price)>> GetServicesWithPricesByAppointmentId(int appointmentId)
        {
            var ordersWithServices = dbContext.Orders
                .Where(o => o.AppointmentId == appointmentId)
                .Join(dbContext.Services,
                      order => order.ServiceId,
                      service => service.ServiceId,
                      (order, service) => new { order.AppointmentId, service.ServiceName, service.Price })
                .ToList();

            if (ordersWithServices != null && ordersWithServices.Any())
            {
                var result = new Dictionary<int, List<(string ServiceName, decimal Price)>>();

                result[appointmentId] = ordersWithServices
                    .Select(o => (o.ServiceName, o.Price ?? 0))
                    .ToList();

                return result;
            }

            return null;
        }



    }
}