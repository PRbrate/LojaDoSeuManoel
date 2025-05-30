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
                entity.UpdatedAt = DateTime.UtcNow;

                return await SaveChanges() > 0;
            }

            public virtual async Task<bool> Delete(Guid id)
            {
                var entity = await DbSet.FindAsync(id);
                if (entity == null)
                    return false;

                DbSet.Remove(entity);
                return await SaveChanges() > 0;
            }

            public async Task<int> SaveChanges() => await _context.SaveChangesAsync();

            public void Dispose() =>
           _context?.Dispose();

        }
    }
}
