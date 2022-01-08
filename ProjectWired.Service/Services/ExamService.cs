using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWired.Core.Models;
using ProjectWired.Core.Repositories;
using ProjectWired.Core.Services;
using ProjectWired.Core.UnitOfWorks;

namespace ProjectWired.Service.Services
{
    public class ExamService : Service<Exam>, IExamService
    {
        public ExamService(IUnitOfWork unitOfWork, IRepository<Exam> repository) : base(unitOfWork, repository)
        {
        }

        public async Task<Exam> GetWithQuestionsAndChoicesByIdAsync(int examId)
        {
            return await _unitOfWork.Exams.GetWithQuestionsAndChoicesByIdAsync(examId);
        }
    }
}
