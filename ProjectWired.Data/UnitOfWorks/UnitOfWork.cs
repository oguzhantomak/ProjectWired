using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWired.Core.Repositories;
using ProjectWired.Core.UnitOfWorks;
using ProjectWired.Data.Repositories;

namespace ProjectWired.Data.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly WiredDbContext _context;
        private ExamRepository _examRepository;
        public UnitOfWork(WiredDbContext wiredDbContext)
        {
            _context = wiredDbContext;
        }

        public IExamRepository Exams => _examRepository = _examRepository ?? new ExamRepository(_context);
        public async Task CommitAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
