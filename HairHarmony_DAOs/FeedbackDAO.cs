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
        public List<Feedback> getFeedbackByStylistID(string id)
        {
            List<Feedback> feed = dbContext.Feedbacks.ToList();
            feed.RemoveAll(f => f.StylistId != id);
            
            return feed;    
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
                .FirstOrDefault(f => f.AppointmentId == appointmentId);

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
                };
                dbContext.Feedbacks.Add(feedbackEntry);
            }
            dbContext.SaveChanges();
        }

        public Feedback getFeedbackByAppoinIdAndServiceId(int appointmentId, int serviceId)
        {
            return dbContext.Feedbacks.FirstOrDefault(f => f.AppointmentId == appointmentId && f.ServiceId == serviceId);
        }

        public void SaveFeedback(int appointmentId, int serviceId, string comments, int rating, string stylistId)
        {
            try
            {
                var feedback = dbContext.Feedbacks.FirstOrDefault(f => f.AppointmentId == appointmentId && f.ServiceId == serviceId);

                if (feedback == null)
                {
                    int newFeedbackId = dbContext.Feedbacks.Any() ? dbContext.Feedbacks.Max(f => f.FeedbackId) + 1 : 1;
                    feedback = new Feedback
                    {
                        FeedbackId = newFeedbackId,
                        AppointmentId = appointmentId,
                        ServiceId = serviceId,
                        Comments = comments,
                        Rating = rating,
                        StylistId = stylistId
                    };

                    dbContext.Feedbacks.Add(feedback);
                    dbContext.SaveChanges();
                    var stylistFeedbacks = dbContext.Feedbacks.Where(f => f.StylistId == stylistId).ToList();

                    if (stylistFeedbacks.Any())
                    {
                        var averageRating = stylistFeedbacks.Average(f => f.Rating);
                        var roundedUpRating = (int)Math.Ceiling((decimal)averageRating);

                        var stylistAccount = dbContext.Accounts.FirstOrDefault(a => a.AccountId == stylistId);
                        if (stylistAccount != null)
                        {
                            stylistAccount.LoyaltyPoints = roundedUpRating;

                            dbContext.Update(stylistAccount);
                            dbContext.SaveChanges();
                        }
                        else
                        {
                            throw new Exception($"Stylist with ID {stylistId} not found in Accounts.");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"No feedbacks found for stylist with ID {stylistId}.");
                    }
                }
                else
                {
                    feedback.Comments = comments;
                    feedback.Rating = rating;
                    feedback.StylistId = stylistId;
                    dbContext.Update(feedback);
                    dbContext.SaveChanges();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while saving feedback.", ex);
            }
        }

    }
}
