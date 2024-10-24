using HairHarmony_BusinessObject;
using HairHarmony_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Repository
{
    public class StylistServiceRepository: IStylistServiceRepo
    {
        public List<StylistService> getListServiceStylist() => StylistServiceDAO.Instance.getListServiceStylist();

        public StylistService GetStylistServiceByStylistID(string id) => StylistServiceDAO.Instance.GetStylistServiceByStylistID(id);
    }
}
