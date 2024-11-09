using HairHarmony_BusinessObject;
using HairHarmony_DAOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Repository
{
    public class FeedbackRepository : IFeedbackRepository
    {
        public bool deleteFeedback(int feedbackID) => FeedbackDAO.Instance.deleteFeedback(feedbackID);
       

        public Feedback getFeedbackByAppoinId(int id) => FeedbackDAO.Instance.getFeedbackByAppoinId(id);

        public Feedback searchFeedback(int feedbackID) => FeedbackDAO.Instance.searchFeedback(feedbackID);
        public void SaveFeedback(int appointmentId, string feedback, int points, string cus)
        {
            FeedbackDAO.Instance.SaveFeedback(appointmentId, feedback, points, cus);
        }
        public List<Feedback> getFeedbackByStylistID(string id) => FeedbackDAO.Instance.getFeedbackByStylistID(id);

        public void SaveFeedback(int appointmentId, int serviceId, string comments, int rating, string stylistId)
        {
            FeedbackDAO.Instance.SaveFeedback(appointmentId, serviceId, comments, rating, stylistId);
        }

        public Feedback getFeedbackByAppoinIdAndServiceId(int appointmentId, int serviceId)
        {
            return FeedbackDAO.Instance.getFeedbackByAppoinIdAndServiceId(appointmentId, serviceId);
        }

    }
}
