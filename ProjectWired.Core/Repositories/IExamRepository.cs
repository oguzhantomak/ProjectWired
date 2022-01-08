using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWired.Core.DTOs;
using ProjectWired.Core.Models;

namespace ProjectWired.Core.Repositories
{
    public interface IExamRepository : IRepository<Exam>
    {
        Task<Exam> GetWithQuestionsAndChoicesByIdAsync(int examId);

        //Task<Exam> CreateExamWithQuestionsAndChoices(CreateExamDto model);
    }
}
