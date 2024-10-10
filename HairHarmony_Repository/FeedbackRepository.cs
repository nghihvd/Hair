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
        public List<Feedback> getFeedBacksById(string id) => FeedbackDAO.Instance.getFeedBacksById(id);
        
    }
}
