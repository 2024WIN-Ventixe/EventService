using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Presentation.Data.Entities;
using Presentation.Data.Models;

namespace Presentation.Data.Repositories;

public class EventRespository(DataContext context) : BaseRepository<EventEntity>(context), IEventRespository
{
    public override async Task<RepositoryResult<IEnumerable<EventEntity>>> GetAllAsync()
    {
        try
        {
            var entities = await _table.Include(x => x.Packages).ToListAsync();
            return new RepositoryResult<IEnumerable<EventEntity>> { Success = true, Result = entities };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<EventEntity>> { Success = false, Error = ex.Message, };
        }
    }

    public override async Task<RepositoryResult<EventEntity?>> GetAsync(Expression<Func<EventEntity, bool>> expression)
    {
        try
        {
            var entity = await _table.Include(x => x.Packages).FirstOrDefaultAsync(expression);
            return entity == null ? throw new Exception("Not Found.") : new RepositoryResult<EventEntity?> { Success = true, Result = entity, };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<EventEntity?> { Success = false, Error = ex.Message, };
        }
    }

}
