using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace LojaDoSeuManoel.Core
{

    namespace BarberShop.Core
    {
        public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : Entity, new()
        {
            private readonly DbSet<TEntity> DbSet;
            private readonly DbContext _context;
            public RepositoryBase(DbContext context)
            {
                _context = context;
                DbSet = context.Set<TEntity>();
            }
            public virtual async Task<TEntity> GetId(Guid id) => await DbSet.FindAsync(id);
            public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate) => await DbSet.AsNoTrackingWithIdentityResolution().Where(predicate).ToListAsync();
            public virtual async Task<bool> Create(TEntity entity)
            {
                DbSet.Add(entity);
                return await SaveChanges() > 0;
            }

            public async Task<TEntity> GetById(Guid id)
            {
                return await DbSet.FindAsync(id);
            }
            public virtual async Task<bool> Update(TEntity entity)
            {
                _context.Entry(entity).State = EntityState.Modified;

                return await SaveChanges() > 0;
            }
            public virtual async Task<bool> UpdateList(List<TEntity> entities)
            {
                entities.ForEach(entity => _context.Entry(entity).State = EntityState.Modified);

                return await SaveChanges() > 0;
            }
            public virtual async Task<bool> Delete(Guid id)
            {
                DbSet.Remove(new TEntity { Id = id });
                return await SaveChanges() > 0;
            }

            public async Task<int> SaveChanges() => await _context.SaveChangesAsync();

            public void Dispose() =>
           _context?.Dispose();

        }
    }
}
