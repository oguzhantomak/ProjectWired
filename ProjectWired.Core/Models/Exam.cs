using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWired.Core.Models
{
    public class Exam : BaseClass
    {
        public string ExamHeader { get; set; }

        public string ExamDetail { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
