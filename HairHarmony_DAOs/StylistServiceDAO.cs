﻿using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_DAOs
{
    public class StylistServiceDAO
    {
        private static HairContext dbContext;
        private static StylistServiceDAO instance = null;

        public StylistServiceDAO()
        {
            dbContext = new HairContext();
        }

        public static StylistServiceDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new StylistServiceDAO();
                }
                return instance;
            }
        }

        public List<StylistService> getListServiceStylist()
        {
            return dbContext.StylistServices.ToList();
        }


        public List<StylistService> GetStylistServiceByStylistID(string id)
        {
            List<StylistService> stylistServices = getListServiceStylist();
            foreach (StylistService stylistService in stylistServices)
            {
                if (!stylistService.StylistId.Equals(id))
                {
                    stylistServices.Remove(stylistService);
                }
            }
            return stylistServices;

        }

        public StylistService GetStylistServiceByStylistIDAndServiceID(string id, int serviceId)
        {
            return dbContext.StylistServices.SingleOrDefault(a => a.StylistId.Equals(id) && a.ServiceId == serviceId);
        }

        public bool DisableServiceStylist(string stylistID, int serviceID)
        {
            bool result = false;
            StylistService stylist = GetStylistServiceByStylistIDAndServiceID(stylistID, serviceID);
            if (stylist.Status)
            {
                stylist.Status = false;
                dbContext.Update(stylist);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool EnableServiceStylist(string stylistID, int serviceID)
        {
            bool result = false;
            StylistService stylist = GetStylistServiceByStylistIDAndServiceID(stylistID, serviceID);
            if (!stylist.Status)
            {
                stylist.Status = true;
                dbContext.Update(stylist);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool AddMoreServiceOfStylist(StylistService sty)
        {
            bool result = false;
            StylistService stylist = GetStylistServiceByStylistIDAndServiceID(sty.StylistId, sty.ServiceId);
            if (stylist == null)
            {
                dbContext.Add(sty);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool UpdateComission(string stylisID, int serviceID, double comissionRate)
        {
            bool result = false;
            StylistService stylistService = GetStylistServiceByStylistIDAndServiceID(stylisID, serviceID);
            if (stylistService != null)
            {
                stylistService.CommissionRate = comissionRate;
                dbContext.Update(stylistService);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

    }
}
