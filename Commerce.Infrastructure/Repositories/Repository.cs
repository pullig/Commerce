using Commerce.Domain.Interfaces.Repositories;
using Commerce.Infrastructure.Context;
using System;
using System.Threading.Tasks;

namespace Commerce.Infrastructure.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly CommerceContext context;

        public Repository(CommerceContext context)
        {
            this.context = context;
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await context.Set<TEntity>().AddAsync(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be saved: {ex.Message}");
            }

        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException($"{nameof(UpdateAsync)} entity must not be null");
            }

            try
            {
                context.Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be updated: {ex.Message}");
            }
        }

        public TEntity GetById(int id)
        {
            TEntity entity;

            if (id == 0)
            {
                throw new ArgumentNullException($"{nameof(GetById)} Id must not be 0");
            }

            try
            {
                entity = context.Set<TEntity>().Find(id);

                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} could not be searched: {ex.Message}");
            }
        }
    }
}
