using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;

namespace HairHarmony_Services
{
    public interface IServiceService
    {
        public Service GetServiceByID(int ServiceId);

        public List<Service> GetServiceList();
        public bool AddService(Service service);
        public bool DeleteService(int ServiceId);
        public bool UpdateService(Service service);

        public bool DisableService(int ServiceId);

        public List<Service> ShowServiceForCustomer();

        public Service GetServiceByName(string name);
    }
}