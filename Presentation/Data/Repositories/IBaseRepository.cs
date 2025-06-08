using System.Linq.Expressions;
using Presentation.Data.Models;

namespace Presentation.Data.Repositories
{
    public interface IBaseRepository<TEntity> where TEntity : class
    {
        Task<RepositoryResult> AddAsync(TEntity entity);
        Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> expression);
    }
}