using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Repository
{
    interface IStylistServiceRepo
    {
        public List<StylistService> getListServiceStylist();

        public StylistService GetStylistServiceByStylistID(string id);

        public StylistService GetStylistServiceByStylistIDAndServiceID(string id, int serviceId);
        public bool DisableServiceStylist(string stylistID, int serviceID);

        public bool EnableServiceStylist(string stylistID, int serviceID);
    }
}
