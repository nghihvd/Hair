﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;

namespace HairHarmony_Repository
{
    public  interface IAppointmentRepository
    {
        public List<Appointment> GetAllByStatusUnfinished();
        public Appointment GetById(int appointmentid);
        public Appointment RemoveByID(int appointmentid);
        public List<Appointment> getAppointmentByStylistID(string stylistID);
    }
}
