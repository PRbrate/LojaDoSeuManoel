using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LojaDoSeuManoel.Core
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
        Task<bool> Create(TEntity objeto);
        Task<bool> Update(TEntity objeto);
        Task<bool> Delete(Guid id);

        Task<TEntity> GetById(Guid id);
        Task<int> SaveChanges();

    }
}
