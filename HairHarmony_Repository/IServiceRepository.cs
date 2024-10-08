using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HairHarmony_BusinessObject;

namespace HairHarmony_Repository
{
    public interface IServiceRepository
    {
        public Service GetServiceByID(int ServiceId);

        public List<Service> GetServiceList();


    }
}
