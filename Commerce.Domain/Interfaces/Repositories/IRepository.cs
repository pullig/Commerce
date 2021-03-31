using System.Threading.Tasks;

namespace Commerce.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Add an entity to the database
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>The entity that was created</returns>
        public Task<TEntity> AddAsync(TEntity entity);
    }
}
