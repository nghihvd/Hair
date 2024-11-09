using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;
using HairHarmony_Repository;

namespace HairHarmony_Services
{
    public class ServiceService : IServiceService
    {
        private IServiceRepository serviceRepo;
        public ServiceService()
        {
            serviceRepo = new ServiceRepository();
        }

        public Service GetServiceByID(int ServiceId)
        {
            return serviceRepo.GetServiceByID(ServiceId);
        }

        public List<Service> GetServiceList()
        {
            return serviceRepo.GetServiceList();
        }

        public bool AddService(Service service)
        {
            return serviceRepo.AddService(service);
        }

        public bool DeleteService(int ServiceId)
        {
            return serviceRepo.DeleteService(ServiceId);
        }

        public bool UpdateService(Service service)
        {
            return serviceRepo.UpdateService(service);
        }

        public bool DisableService(int ServiceId)
        {
            return serviceRepo.DisableService(ServiceId);
        }

        public List<Service> ShowServiceForCustomer()
        {
            return serviceRepo.ShowServiceForCustomer();
        }
    }
}