using HairHarmony_BusinessObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HairHarmony_Repository
{
    public interface IFeedbackRepository
    {
        public List<Feedback> getFeedBacksById(string id);
    }
}
