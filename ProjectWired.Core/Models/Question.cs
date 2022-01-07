using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWired.Core.Models
{
    public class Question : BaseClass
    {
        public string QuestionHeader { get; set; }

        public int CorrectAnswerId { get; set; }

        public int SortOrder { get; set; }

        public int ExamId { get; set; }
        public virtual Exam Exam { get; set; }

        public virtual ICollection<Choice> Choices { get; set; }

    }
}
