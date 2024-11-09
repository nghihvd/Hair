using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HairHarmony_DAOs
{
    public class ShiftDAO
    {
        private HairContext dbContext;
        private static ShiftDAO instance = null;

        public static ShiftDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ShiftDAO();
                }
                return instance;
            }
        }

        public ShiftDAO()
        {
            dbContext = new HairContext();
        }

        // Lấy tất cả các shift
        public List<Shift> GetAllShifts()
        {
            return dbContext.Shifts.ToList();
        }

        // Lấy các shift theo stylist
        public List<Shift> GetShiftsByStylist(string stylistId)
        {
            return dbContext.Shifts.Where(s => s.StylistId == stylistId).ToList();
        }

        // Kiểm tra xem stylist có rảnh trong khoảng thời gian đã chọn không
        public bool IsStylistAvailable(string stylistId, DateTime date, TimeSpan startTime, TimeSpan endTime)
        {
            var shifts = GetShiftsByStylist(stylistId)
                .Where(s => s.Date == date)
                .ToList();

            foreach (var shift in shifts)
            {
                if ((startTime < shift.EndTime && endTime > shift.StartTime) ||
                    (endTime > shift.StartTime && startTime < shift.EndTime))
                {
                    return false;
                }
            }
            return true;
        }

        // Thêm shift mới
        public void AddShift(Shift shift)
        {
            dbContext.Shifts.Add(shift);
            dbContext.SaveChanges();
        }

        // Cập nhật shift
        public void UpdateShift(Shift shift)
        {
            var existingShift = dbContext.Shifts.Find(shift.ShiftId);
            if (existingShift != null)
            {
                existingShift.StartTime = shift.StartTime;
                existingShift.EndTime = shift.EndTime;
                existingShift.Date = shift.Date;
                dbContext.SaveChanges();
            }
        }
    }
}
