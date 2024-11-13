using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;
using HairHarmony_DAOs;

namespace HairHarmony_Repository
{
    public class ServiceRepository : IServiceRepository
    {
        public Service GetServiceByID(int ServiceId) => ServiceDAO.Instance.GetServiceByID(ServiceId);


        public List<Service> GetServiceList() => ServiceDAO.Instance.GetServiceList();
        public bool AddService(Service service) => ServiceDAO.Instance.AddService(service);

        public bool DeleteService(int ServiceId) => ServiceDAO.Instance.DeleteService(ServiceId);


        public bool UpdateService(Service service) => ServiceDAO.Instance.UpdateService(service);

        public bool DisableService(int ServiceId) => ServiceDAO.Instance.DisableService(ServiceId);

        public List<Service> ShowServiceForCustomer() => ServiceDAO.Instance.ShowServiceForCustomer();

        public Service GetServiceByName(string name) => ServiceDAO.Instance.GetServiceByName(name);

    }
}