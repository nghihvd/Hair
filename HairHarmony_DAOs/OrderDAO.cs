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

        public Dictionary<int, List<(string serviceName, string stylistID)>> GetServiceNamesAndStylistByAppointmentId(int appointmentId)
        {
            var ordersWithServices = dbContext.Orders
                .Where(o => o.AppointmentId == appointmentId)
                .Join(dbContext.Services,
                    order => order.ServiceId,
                    service => service.ServiceId,
                    (order, service) => new { order.AppointmentId, ServiceName = service.ServiceName, StylistID = order.StylistId })
                .ToList();

            if (ordersWithServices != null && ordersWithServices.Any())
            {
                var result = new Dictionary<int, List<(string ServiceName, string stylistID)>>();
                result[appointmentId] = ordersWithServices.Select(o => (o.ServiceName, o.StylistID)).ToList();
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

        public Dictionary<int, List<decimal?>> GetPriceWithServiceIDByAppointmentID(int appointmentId)
        {
            var orderWithService = dbContext.Orders
                .Where(o => o.AppointmentId == appointmentId)
                .Join(dbContext.Services, order => order.ServiceId, service => service.ServiceId, (order, service) => new { order.AppointmentId, servicePrice = service.Price }).ToList();

            if (orderWithService != null && orderWithService.Any())
            {
                var result = new Dictionary<int, List<decimal?>>();
                result[appointmentId] = orderWithService.Select(o => o.servicePrice).ToList();
                return result;
            }
            return null;
        }

        public (string StylistName, List<(string ServiceName, decimal Price)> Services) GetServicesWithPricesByAppointmentId(int appointmentId)
        {
            var orders = dbContext.Orders
                .Where(o => o.AppointmentId == appointmentId)
                .ToList();

            var services = orders
                .Join(dbContext.Services,
                      order => order.ServiceId,
                      service => service.ServiceId,
                      (order, service) => new { order.StylistId, service.ServiceName, service.Price })
                .ToList();

            var stylistId = services.FirstOrDefault()?.StylistId;
            var stylistName = dbContext.Accounts
                .Where(a => a.AccountId == stylistId)
                .Select(a => a.Name)
                .FirstOrDefault() ?? "Unknown Stylist";

            var serviceList = services
                .Select(o => (o.ServiceName, o.Price ?? 0))
                .ToList();

            return (stylistName, serviceList);
        }

        public List<Order> GetOrderByStylistIDAndServiceID(string stylistId, int serviceID)
        {
            List<Order> orders = this.GetAllOrders();
            orders.RemoveAll(a => !a.StylistId.Equals(stylistId) && a.ServiceId != serviceID);
            return orders;
        }

        public void CreateOrder(Order order)
        {
            dbContext.Orders.Add(order);
            dbContext.SaveChanges();
        }
    }



}