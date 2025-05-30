namespace LojaDoSeuManoel.Core
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : Entity
    {
        Task<bool> Create(TEntity objeto);
        Task<bool> Update(TEntity objeto);
        Task<bool> Delete(Guid id);

        Task<TEntity> GetById(Guid id);
        Task<int> SaveChanges();

    }
}
