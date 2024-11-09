using HairHarmony_BusinessObject;
using HairHarmony_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Services
{
    public class ShiftService : IShiftService
    {
        private IShiftRepository shiftRepo;

        public ShiftService()
        {
            shiftRepo = new ShiftRepository();
        }

        public List<Shift> GetAllShifts() => shiftRepo.GetAllShifts();

        public List<Shift> GetShiftsByStylist(string stylistId) => shiftRepo.GetShiftsByStylist(stylistId);

        public bool IsStylistAvailable(string stylistId, DateTime date, TimeSpan startTime, TimeSpan endTime)
            => shiftRepo.IsStylistAvailable(stylistId, date, startTime, endTime);

        public void AddShift(Shift shift) => shiftRepo.AddShift(shift);

        public void UpdateShift(Shift shift) => shiftRepo.UpdateShift(shift);
    }
}
