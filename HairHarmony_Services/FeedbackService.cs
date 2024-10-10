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
        public List<Feedback> getFeedbackById(string id)
        {
            return repository.getFeedBacksById(id);
        }
    }
}
