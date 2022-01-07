using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWired.Core.Models;

namespace ProjectWired.Core.Services
{
    public interface IExamService : IService<Exam>
    {
        Task<Exam> GetWithQuestionsAndChoicesByIdAsync(int examId);

    }
}
