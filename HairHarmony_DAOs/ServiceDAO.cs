using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;

namespace HairHarmony_DAOs
{
    public class ServiceDAO
    {
        private HairContext dbContext;
        private static ServiceDAO instance = null;

        public static ServiceDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceDAO();
                }
                return instance;
            }
        }

        public ServiceDAO()
        {
            dbContext = new HairContext();
        }

        public List<Service> GetServiceList()
        {
            return dbContext.Services.ToList();
        }

        public Service GetServiceByID(int ServiceId)
        {
            return dbContext.Services.SingleOrDefault(a => a.ServiceId.Equals(ServiceId));
        }

        public bool addService(Service service)
        {
            bool result = false;    
            Service search = GetServiceByID(service.ServiceId);
            if (search == null)
            {
                dbContext.Services.Add(service);
                dbContext.SaveChanges();
                result = true;
            }
            return result;

        }

    }
}
