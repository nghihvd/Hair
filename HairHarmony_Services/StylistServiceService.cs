﻿using HairHarmony_BusinessObject;
using HairHarmony_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Services
{
    public class StylistServiceService : IStylistServiceService
    {
        private StylistServiceRepository repo;
        public StylistServiceService()
        {
            repo = new StylistServiceRepository();
        }
        public List<StylistService> getListServiceStylist()
        {
            return repo.getListServiceStylist();    
        }
        public List<StylistService> GetStylistServiceByStylistID(string id)
        {
            return repo.GetStylistServiceByStylistID(id);
        }

        public StylistService GetStylistServiceByStylistIDAndServiceID(string id, int serviceId)
        {
            return repo.GetStylistServiceByStylistIDAndServiceID(id, serviceId);
        }

        public bool DisableServiceStylist(string stylistID, int serviceID)
        {
            return repo.DisableServiceStylist(stylistID, serviceID);
        }

        public bool EnableServiceStylist(string stylistID, int serviceID)
        {
            return repo.EnableServiceStylist(stylistID, serviceID); 
        }

        public bool AddMoreServiceOfStylist(StylistService sty)
        {
            return repo.AddMoreServiceOfStylist(sty);
        }

        public bool UpdateComission(string stylistID, int serviceID, double commission)
        {
            return repo.UpdateComission(stylistID,serviceID,commission);
        }

        public List<StylistService> GetListStylistByServiceID(int serviceID)
        {
            return repo.GetListStylistByServiceID(serviceID);
        }

        public bool Delete(string stylistID, int serviceID)
        {
            return repo.Delete(stylistID,serviceID);
        }

        public StylistService GetStylisServiceByStylistId(String stylistId)
        {
            return repo.GetStylisServiceByStylistId(stylistId);
        }
        public List<int> GetServiceIdsByStylistId(string stylistId)
        {
            return repo.GetServiceIdsByStylistId(stylistId);
        }


        public List<StylistService> GetListAvailableStylistByServiceID(int serviceID)
        {
            return repo.GetListAvailableStylistByServiceID(serviceID);
        }

    }
}
