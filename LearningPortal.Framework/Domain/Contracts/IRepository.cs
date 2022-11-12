using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LearningPortal.Framework.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        DbSet<TEntity> dbEntities { get; }

        IQueryable<TEntity> Get { get; }
        IQueryable<TEntity> GetAsNoTracking { get; }

        Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true);
        Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool AutoSave = true);

        Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true);
        Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool AutoSave = true);

        Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true);
        Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool AutoSave = true);

        Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] Ids);

        Task<int> SaveChangeAsync(CancellationToken cancellationToken = default);
    }
}
