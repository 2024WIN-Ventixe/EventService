using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Presentation.Data.Models;

namespace Presentation.Data.Repositories;

public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
{
    protected readonly DataContext _context;
    protected readonly DbSet<TEntity> _table;
    protected BaseRepository(DataContext context)
    {
        _context = context;
        _table = _context.Set<TEntity>();
    }

    public virtual async Task<RepositoryResult<IEnumerable<TEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.ToListAsync();
            return new RepositoryResult<IEnumerable<TEntity>> { Success = true, Result = entities };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<TEntity>> { Success = false, Error = ex.Message, };
        }

    }

    public virtual async Task<RepositoryResult<TEntity?>> GetAsync(Expression<Func<TEntity, bool>> expression)
    {
        try
        {
            var entity = await _table.FirstOrDefaultAsync(expression);
            return entity == null ? throw new Exception("Not Found.") : new RepositoryResult<TEntity?> { Success = true, Result = entity, };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<TEntity?> { Success = false, Error = ex.Message, };
        }

    }
    public virtual async Task<RepositoryResult> AddAsync(TEntity entity)
    {
        try
        {
            _table.Add(entity);
            await _context.SaveChangesAsync();

            return new RepositoryResult
            {
                Success = true,
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult
            {
                Success = false,
                Error = ex.Message,
            };
        }

    }

}
