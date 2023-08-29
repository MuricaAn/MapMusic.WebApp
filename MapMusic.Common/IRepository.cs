using System.Security.Principal;

namespace MapMusic.Common
{
    public interface IRepository<TEntity>
        where TEntity : class, IEntity
    {
        IQueryable<TEntity> Get();
        TEntity Insert(TEntity entity);
        TEntity Update(TEntity entitty);
        void Delete(TEntity entity);
    }
}