﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;
using HairHarmony_Repository;

namespace HairHarmony_Services
{
    public class AppointmentService : IAppointmentService
    {

        private IAppointmentRepository appointmentRepo;


        public AppointmentService()
        {
            appointmentRepo = new AppointmentRepository();
        }

        public List<Appointment> GetAllByStatusUnfinished()
        {
        return appointmentRepo.GetAllByStatusUnfinished();
        }

        public Appointment GetById(int appointmentid)
        {
            return appointmentRepo.GetById(appointmentid);
        }

        public Appointment RemoveByID(int appointmentid)
        {
            return appointmentRepo.RemoveByID(appointmentid);
        }
    }
}