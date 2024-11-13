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

        public List<StylistService> GetStylistServiceByStylistID(string id) => StylistServiceDAO.Instance.GetStylistServiceByStylistID(id);
        public StylistService GetStylistServiceByStylistIDAndServiceID(string id, int serviceId) => StylistServiceDAO.Instance.GetStylistServiceByStylistIDAndServiceID(id, serviceId);

        public bool DisableServiceStylist(string stylistID, int serviceID) => StylistServiceDAO.Instance.DisableServiceStylist(stylistID, serviceID);

        public bool EnableServiceStylist(string stylistID, int serviceID) => StylistServiceDAO.Instance.EnableServiceStylist(stylistID,serviceID);

        public bool AddMoreServiceOfStylist(StylistService sty) => StylistServiceDAO.Instance.AddMoreServiceOfStylist(sty);
        public bool UpdateComission(string stylistID, int serviceID, double commission) => StylistServiceDAO.Instance.UpdateComission(stylistID,serviceID,commission);

        public List<StylistService> GetListStylistByServiceID(int serviceID) => StylistServiceDAO.Instance.GetListStylistByServiceID(serviceID);

        public bool Delete(string stylistID, int serviceID) => StylistServiceDAO.Instance.Delete(stylistID, serviceID);
        public StylistService GetStylisServiceByStylistId(String stylistId) => StylistServiceDAO.Instance.GetStylisServiceByStylistId(stylistId);

        public List<StylistService> GetListAvailableStylistByServiceID(int serviceID) => StylistServiceDAO.Instance.GetListAvailableStylistByServiceID(serviceID);
    }
}
