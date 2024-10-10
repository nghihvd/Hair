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

        public List<Feedback> getFeedBacksById(string id)
        {
            List <Feedback> feedbacks = dbContext.Feedbacks.ToList();
            foreach(Feedback f in feedbacks) 
            {
                if (!f.CustomerId.Equals(id))
                {
                    feedbacks.Remove(f);
                }
            }
            return feedbacks;
        }
    }
}
