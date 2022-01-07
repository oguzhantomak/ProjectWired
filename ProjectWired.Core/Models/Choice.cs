using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWired.Core.Models
{
    public class Choice : BaseClass
    {
        public string ChoiceText { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
    }
}
