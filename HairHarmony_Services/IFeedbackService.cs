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
    }
}
