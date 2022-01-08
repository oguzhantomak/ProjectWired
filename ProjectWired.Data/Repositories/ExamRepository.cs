using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjectWired.Core.DTOs;
using ProjectWired.Core.Models;
using ProjectWired.Core.Repositories;

namespace ProjectWired.Data.Repositories
{
    public class ExamRepository : Repository<Exam>, IExamRepository
    {
        private WiredDbContext _wiredDbContext { get => _context as WiredDbContext; }
        public ExamRepository(WiredDbContext context) : base(context)
        {
        }

        public async Task<Exam> GetWithQuestionsAndChoicesByIdAsync(int examId)
        {
            return await _wiredDbContext.Exams
                .Include(x => x.Questions)
                .ThenInclude(x => x.Choices)
                .SingleOrDefaultAsync(x => x.Id == examId);
            
        }

        //public async Task<Exam> CreateExamWithQuestionsAndChoices(CreateExamDto model)
        //{
            
        //}
    }
}
