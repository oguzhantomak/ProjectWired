using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWired.Core.Models;

namespace ProjectWired.Core.DTOs
{
    public  class CreateQuestionDto
    {
        public string QuestionHeader { get; set; }

        public string CorrectChoiceKey { get; set; }

        public int SortOrder { get; set; }
        
        public List<CreateChoice> Choices { get; set; }
    }
}
