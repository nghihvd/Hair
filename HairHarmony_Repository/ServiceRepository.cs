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
        
    }
}
