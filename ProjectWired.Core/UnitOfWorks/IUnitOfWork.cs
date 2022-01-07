using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProjectWired.Core.Repositories;

namespace ProjectWired.Core.UnitOfWorks
{
    public  interface IUnitOfWork
    {
        IExamRepository Exams { get; }

        Task CommitAsync();

        void Commit();
    }
}
