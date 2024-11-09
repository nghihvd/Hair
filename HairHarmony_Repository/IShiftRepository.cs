﻿using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Repository
{
    public interface IShiftRepository
    {
        List<Shift> GetAllShifts();
        List<Shift> GetShiftsByStylist(string stylistId);
        bool IsStylistAvailable(string stylistId, DateTime date, TimeSpan startTime, TimeSpan endTime);
        void AddShift(Shift shift);
        void UpdateShift(Shift shift);
    }
}