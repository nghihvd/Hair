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
    }
}
