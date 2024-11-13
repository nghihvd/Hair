using HairHarmony_BusinessObject;
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
            stylistServices.RemoveAll(a => !a.StylistId.Equals(id));
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

        public bool Delete(string stylistID, int serviceID)
        {
            bool result = false;
            StylistService stylist = GetStylistServiceByStylistIDAndServiceID(stylistID, serviceID);
            if (stylist!= null)
            {
                dbContext.Remove(stylist);
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
        public bool UpdateComission(string stylistID, int serviceID, double commission)
        {
            bool result = false;
            StylistService search = GetStylistServiceByStylistIDAndServiceID(stylistID,serviceID);
            if (search != null)
            {
                search.CommissionRate = commission;
                dbContext.Update(search);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public List<StylistService> GetListStylistByServiceID(int serviceID)
        {
            List<StylistService> stylistServices = getListServiceStylist();
            stylistServices.RemoveAll(a => a.ServiceId != serviceID);
            
            return stylistServices;
        }


        public StylistService GetStylisServiceByStylistId(String stylistId)
        {
            return dbContext.StylistServices.SingleOrDefault(m => m.StylistId.Equals(stylistId));
        }

    }
}
