using HairHarmony_BusinessObject;
using HairHarmony_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Repository
{
    public class ShiftRepository : IShiftRepository
    {
        public List<Shift> GetAllShifts() => ShiftDAO.Instance.GetAllShifts();

        public List<Shift> GetShiftsByStylist(string stylistId) => ShiftDAO.Instance.GetShiftsByStylist(stylistId);

        public bool IsStylistAvailable(string stylistId, DateTime date, TimeSpan startTime, TimeSpan endTime)
            => ShiftDAO.Instance.IsStylistAvailable(stylistId, date, startTime, endTime);

        public void AddShift(Shift shift) => ShiftDAO.Instance.AddShift(shift);

        public void UpdateShift(Shift shift) => ShiftDAO.Instance.UpdateShift(shift);

        public void DeleteAllShifts(List<Shift> shifts) =>ShiftDAO.Instance.DeleteAllShifts(shifts);


    }
}
