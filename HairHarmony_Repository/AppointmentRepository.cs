using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;
using HairHarmony_DAOs;

namespace HairHarmony_Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        public List<Appointment> GetAll() => AppointmentDAO.Instance.GetAll();
        

        public List<Appointment> GetAllByStatusUnfinished() => AppointmentDAO.Instance.GetAllByStatusUnfinished();

        public List<Appointment> getAppointmentByStylistID(string stylistID) => AppointmentDAO.Instance.getAppointmentByStylistID(stylistID);
        

        public Appointment GetById(int appointmentid) => AppointmentDAO.Instance.GetById(appointmentid);
        

        public Appointment RemoveByID(int appointmentid) => AppointmentDAO.Instance.RemoveByID(appointmentid);
       
    }
}
