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
        public StylistService GetStylistServiceByStylistIDAndServiceID(string id, int serviceId) => StylistServiceDAO.Instance.GetStylistServiceByStylistIDAndServiceID(id, serviceId);

        public bool DisableServiceStylist(string stylistID, int serviceID) => StylistServiceDAO.Instance.DisableServiceStylist(stylistID, serviceID);

        public bool EnableServiceStylist(string stylistID, int serviceID) => StylistServiceDAO.Instance.EnableServiceStylist(stylistID,serviceID);

       
    }
}
