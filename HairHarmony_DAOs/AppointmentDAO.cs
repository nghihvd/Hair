﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;
using Microsoft.EntityFrameworkCore;

namespace HairHarmony_DAOs
{
    public  class AppointmentDAO
    {
        private HairContext dbContext;
        private static AppointmentDAO instance = null;
        


        public static AppointmentDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AppointmentDAO();
                }
                return instance;
            }
        }

        public AppointmentDAO()
        {
            dbContext = new HairContext();

        }
        public List<Appointment> GetAllByStatusUnfinished() 
        {
            List<Appointment> appointment = dbContext.Appointments.ToList();

            foreach (Appointment a in appointment) 
            {
                if (!a.Status.Equals("Unfinished"))
                {
                    appointment.Remove(a);
                }            
            }
            return appointment;
        }

        public Appointment GetById(int appointmentid)
        {
            
            return dbContext.Appointments.SingleOrDefault(a=>a.AppointmentId.Equals(appointmentid));
        }
        public Appointment RemoveByID(int appointmentid)
        {
            // Tìm bản ghi Appointment tương ứng
            var appointment = dbContext.Appointments.SingleOrDefault(a => a.AppointmentId.Equals(appointmentid));

            // Kiểm tra xem appointment có null không
            if (appointment == null)
            {
                return null; // hoặc có thể ném ra một ngoại lệ nếu không tìm thấy
            }

            // Xóa appointment
            dbContext.Appointments.Remove(appointment);
            dbContext.SaveChanges(); // Lưu các thay đổi vào cơ sở dữ liệu

            return appointment; // Trả về appointment đã bị xóa
        }

        //public List<Appointment> getAppointmentByStylistID(string stylistID)
        //{
        //    return dbContext.Appointments
        //        .Where(a => a.StylistId.Equals(stylistID))
        //        .ToList();
        //}

        public List<Appointment> GetAll()
        {
            return dbContext.Appointments.ToList();
        }

        public List<Appointment> getAppointmentByCustomerID(string customerId)
        {
            return dbContext.Appointments
                .Where(a => a.CustomerId.Equals(customerId))
                .Include(a => a.Orders)
                .ToList();
        }

        public void UpdateStatus(int appointmentId, string newStatus)
        {
            var appointment = dbContext.Appointments.FirstOrDefault(a => a.AppointmentId == appointmentId);

            if (appointment != null)
            {
                appointment.Status = newStatus;
                dbContext.SaveChanges();
            }
            else
            {
                throw new Exception("Appointment not found.");
            }
        }

        public int GetLastAppointmentId()
        {
            var lastAppointment = dbContext.Appointments.OrderByDescending(a => a.AppointmentId).FirstOrDefault();
            return lastAppointment != null ? lastAppointment.AppointmentId : 0;
        }

        public Appointment CreateNewAppointment(DateTime appointmentDate, string customerId)
        {
            int lastAppointmentId = GetLastAppointmentId();
            int newAppointmentId = lastAppointmentId + 1;

            var newAppointment = new Appointment
            {
                AppointmentId = newAppointmentId,
                AppointmentDate = appointmentDate,
                CustomerId = customerId,
                Status = "Pending" 
            };

            dbContext.Appointments.Add(newAppointment);
            dbContext.SaveChanges();
            return newAppointment;
        }

        public List<Appointment> GetAppointmentsByUserId(string userId)
        {
            return dbContext.Appointments
                .Include(a => a.Orders)
                    .ThenInclude(o => o.S)          // Include StylistService
                    .ThenInclude(s => s.Service)    // Include Service trong StylistService
                .Include(a => a.Orders)
                    .ThenInclude(o => o.S)
                    .ThenInclude(s => s.Stylist)    // Include Stylist trong StylistService
                .Where(a => a.CustomerId == userId)
                .ToList();
        }

    }
}
