﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;

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

    }
}