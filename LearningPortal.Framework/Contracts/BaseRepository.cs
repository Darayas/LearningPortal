using LearningPortal.Framework.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LearningPortal.Framework.Contracts
{
    public class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
    {
        private readonly DbContext dbContext;
        public DbSet<TEntity> dbEntities { get; }

        public BaseRepository(DbContext dbContext)
        {
            this.dbContext=dbContext;
            dbEntities = dbContext.Set<TEntity>();
        }

        public IQueryable<TEntity> Get => dbEntities;

        public IQueryable<TEntity> GetAsNoTracking => dbEntities.AsNoTracking();

        public async Task AddAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true)
        {
            await dbEntities.AddAsync(entity, cancellationToken);
            if (AutoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool AutoSave = true)
        {
            await dbEntities.AddRangeAsync(entities, cancellationToken);
            if (AutoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true)
        {
            dbEntities.Remove(entity);
            if (AutoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task DeleteRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool AutoSave = true)
        {
            dbEntities.RemoveRange(entities);
            if (AutoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task<TEntity> GetByIdAsync(CancellationToken cancellationToken, params object[] Ids)
        {
            return await dbEntities.FindAsync(Ids, cancellationToken);
        }

        public async Task<int> SaveChangeAsync(CancellationToken cancellationToken = default)
        {
            return await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(TEntity entity, CancellationToken cancellationToken, bool AutoSave = true)
        {
            dbEntities.Update(entity);
            if (AutoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken, bool AutoSave = true)
        {
            dbEntities.UpdateRange(entities);
            if (AutoSave)
            {
                await dbContext.SaveChangesAsync(cancellationToken);
            }
        }
    }
}
