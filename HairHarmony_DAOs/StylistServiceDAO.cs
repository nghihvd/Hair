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

        public StylistService GetStylistServiceByStylistID(string id)
        {
            return dbContext.StylistServices.SingleOrDefault(a => a.StylistId.Equals(id));
        }

        public StylistService GetStylistServiceByStylistIDAndServiceID(string id, int serviceId)
        {
            return dbContext.StylistServices.SingleOrDefault(a => a.StylistId.Equals(id) & a.ServiceId == serviceId);
        }
    }
}
