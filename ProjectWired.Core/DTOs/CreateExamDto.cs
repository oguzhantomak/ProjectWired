using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWired.Core.Models;

namespace ProjectWired.Core.DTOs
{
    public class CreateExamDto
    {
        public string ExamHeader { get; set; }

        public string ExamDetail { get; set; }

        public List<Question> Questions { get; set; }
    }
}
