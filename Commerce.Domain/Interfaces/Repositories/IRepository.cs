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
        // <summary>
        /// Update an entity on the database
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>The entity that was updated</returns>
        public Task<TEntity> UpdateAsync(TEntity entity);
        /// <summary>
        /// Get a entity on the database by its id
        /// </summary>
        /// <param name="id">Id of the entity</param>
        /// <returns>Entity</returns>
        public TEntity GetById(int id);
    }
}
