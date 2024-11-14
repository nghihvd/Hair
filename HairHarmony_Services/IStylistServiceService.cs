using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Services
{
    public interface IStylistServiceService
    {
        
        public List<StylistService> getListServiceStylist();

        public List<StylistService> GetStylistServiceByStylistID(string id);

        public StylistService GetStylistServiceByStylistIDAndServiceID(string id, int serviceId);

        public bool DisableServiceStylist(string stylistID, int serviceID);

        public bool EnableServiceStylist(string stylistID, int serviceID);

        public bool AddMoreServiceOfStylist(StylistService sty);
        public bool UpdateComission(string stylistID, int serviceID, double commission);

        public List<StylistService> GetListStylistByServiceID(int serviceID);

        public bool Delete(string stylistID, int serviceID);

        public StylistService GetStylisServiceByStylistId(String stylistId);

        public List<StylistService> GetListAvailableStylistByServiceID(int serviceID);

        public List<int> GetServiceIdsByStylistId(string stylistId);


    }
}
