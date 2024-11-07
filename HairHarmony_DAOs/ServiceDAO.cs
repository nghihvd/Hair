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

        public bool AddService(Service service)
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

        public bool DeleteService(int ServiceId)
        {
            bool result = false;
            Service search = GetServiceByID(ServiceId);
            if(search != null)
            {   
                dbContext.Services.Remove(search);
                dbContext.SaveChanges();
                result=true;
            }
            return result;
        }

        public bool DisableService (int ServiceId)
        {
            bool result = false;
            Service search = GetServiceByID(ServiceId);
            if (search != null)
            {
                search.Duration = 0;
                dbContext.Services.Update(search);
                dbContext.SaveChanges();
                result = true;
            }
            return result;
        }

        public bool UpdateService(Service service)
        {
            bool result = false;
            Service search = GetServiceByID(service.ServiceId);
            if (search != null)
            {
                search.ServiceId = service.ServiceId;
                search.Price = service.Price;
                search.Duration = service.Duration;
                search.ServiceName = service.ServiceName;   
                dbContext.Services.Update(search);
                result = true;
            }
            return result;
        }


        public List<Service> ShowServiceForCustomer()
        {
            List<Service> services = GetServiceList();
            services.RemoveAll(a => a.Duration == 0);
            return services;
        }
        

    }
}
