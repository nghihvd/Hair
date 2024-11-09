using HairHarmony_BusinessObject;
using HairHarmony_Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Services
{
    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository repository;
        public FeedbackService() {
            repository = new FeedbackRepository();
        }

        public bool deleteFeedback(int feedbackID)
        {
            return repository.deleteFeedback(feedbackID);
        }

        public Feedback getFeedbackByAppoinId(int id)
        {
            return repository.getFeedbackByAppoinId(id);
        }

        public Feedback searchFeedback(int feedbackID)
        {
            return repository.searchFeedback(feedbackID);
        }

        public void SaveFeedback(int appointmentId, string feedback, int points, string cus)
        {
            repository.SaveFeedback(appointmentId, feedback, points, cus);
        }

        public List<Feedback> getFeedbackByStylistID(string id)
        {
            return repository.getFeedbackByStylistID(id);
        }

        public void SaveFeedback(int appointmentId, int serviceId, string comments, int rating, string stylistId)
        {
            repository.SaveFeedback(appointmentId, serviceId, comments, rating, stylistId);
        }

        public Feedback getFeedbackByAppoinIdAndServiceId(int appointmentId, int serviceId)
        {
            return repository.getFeedbackByAppoinIdAndServiceId(appointmentId, serviceId);
        }

    }
}
