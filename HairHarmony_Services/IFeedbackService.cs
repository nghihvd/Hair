using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Services
{
    public interface IFeedbackService
    {
        public Feedback getFeedbackByAppoinId(int id);

        public Feedback searchFeedback(int feedbackID);

        public bool deleteFeedback(int feedbackID);
        public void SaveFeedback(int appointmentId, string feedback, int points, string cus);
        public List<Feedback> getFeedbackByStylistID(string id);

        Feedback getFeedbackByAppoinIdAndServiceId(int appointmentId, int serviceId);

        void SaveFeedback(int appointmentId, int serviceId, string comments, int rating, string stylistId);

    }
}
