using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectWired.Core.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);

        void Remove(TEntity entity);

        Task<IEnumerable<TEntity>> GetAllAsync();

        TEntity Update(TEntity entity);
    }
}
