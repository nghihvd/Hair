﻿using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Repository
{
    interface IStylistServiceRepo
    {
        public List<StylistService> getListServiceStylist();

        public StylistService GetStylistServiceByStylistID(string id);
    }
}
