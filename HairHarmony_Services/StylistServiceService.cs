using HairHarmony_BusinessObject;
using HairHarmony_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Services
{
    public class StylistServiceService : IStylistServiceService
    {
        private StylistServiceRepository repo;
        public StylistServiceService()
        {
            repo = new StylistServiceRepository();
        }
        public List<StylistService> getListServiceStylist()
        {
            return repo.getListServiceStylist();    
        }
        public StylistService GetStylistServiceByStylistID(string id)
        {
            return repo.GetStylistServiceByStylistID(id);
        }
    }
}
