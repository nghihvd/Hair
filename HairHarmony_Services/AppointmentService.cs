using System;
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

        public List<Appointment> GetAll()
        {
            return appointmentRepo.GetAll();
        }

        public List<Appointment> GetAllByStatusUnfinished()
        {
            return appointmentRepo.GetAllByStatusUnfinished();
        }

        //public List<Appointment> getAppointmentByStylistID(string stylistID)
        //{
        //    return appointmentRepo.getAppointmentByStylistID(stylistID);
        //}

        

        public Appointment GetById(int appointmentid)
        {
            return appointmentRepo.GetById(appointmentid);
        }

        public Appointment RemoveByID(int appointmentid)
        {
            return appointmentRepo.RemoveByID(appointmentid);
        }

        public List<Appointment> getAppointmentByCustomerID(string customerId)
        {
            return appointmentRepo.getAppointmentByCustomerID(customerId);
        }

        public void UpdateStatus(int appointmentId, string newStatus)
        {
            appointmentRepo.UpdateStatus(appointmentId, newStatus);
        }
    }
}
