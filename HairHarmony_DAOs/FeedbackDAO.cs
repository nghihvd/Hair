using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_DAOs
{
    public class FeedbackDAO
    {
        private HairContext dbContext;
        private static FeedbackDAO instance = null;

        public static FeedbackDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new FeedbackDAO();
                }
                return instance;
            }
        }

        public FeedbackDAO()
        {
            dbContext = new HairContext();
        }

        public Feedback getFeedbackByAppoinId(int id)
        {
            return dbContext.Feedbacks.SingleOrDefault(a => a.AppointmentId == id);
        }

        public Feedback searchFeedback(int feedbackID)
        {
            return dbContext.Feedbacks.SingleOrDefault(a => a.FeedbackId == feedbackID);
        }
        public bool deleteFeedback(int appointmentId)
        {
            var feedback = dbContext.Feedbacks.Where(o => o.AppointmentId == appointmentId).ToList();

            if (feedback != null && feedback.Any()) {
                dbContext.RemoveRange(feedback);
                dbContext.SaveChanges();
                return true;
            }
            return false;
        }

        public void SaveFeedback(int appointmentId, string feedback, int points, string accountId)
        {
            var existingFeedback = dbContext.Feedbacks
                .FirstOrDefault(f => f.AppointmentId == appointmentId && f.CustomerId == accountId);

            if (existingFeedback != null)
            {
                existingFeedback.Comments = feedback;
                existingFeedback.Rating = points;
            }
            else
            {
                var feedbackEntry = new Feedback
                {
                    AppointmentId = appointmentId,
                    Comments = feedback,
                    Rating = points,
                    CustomerId = accountId
                };
                dbContext.Feedbacks.Add(feedbackEntry);
            }
            dbContext.SaveChanges();
        }

    }
}
