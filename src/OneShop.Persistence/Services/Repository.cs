using System;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using OneShop.Persistence.Persistence;
using OneShop.Application.Common.Interfaces;
using OneShop.Domain.Common;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace OneShop.Persistence.Services
{
    public partial class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly ApplicationDbContext context;
        private DbSet<T> entities { get; set; }

        public Repository(ApplicationDbContext context)
        {
            this.context = context;
            entities = context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await entities.ToListAsync();
        }

        public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> expression)
        {
            return await entities.Where(expression).ToListAsync();
        }

        public virtual async Task<T?> GetByIdAsync(long id)
        {
            return await entities.FindAsync(id);
        }

        public async Task AddAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            await entities.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<T> entityList)
        {
            if (entityList == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            await entities.AddRangeAsync(entityList);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            context.Entry(entity).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }

        public async Task<int> UpdateAsync(T entity, params Expression<Func<T, object>>[] navigations)
        {
            var dbEntity = await context.FindAsync<T>(entity.Id);

            var dbEntry = context.Entry(dbEntity);
            dbEntry.CurrentValues.SetValues(entity);

            foreach (var property in navigations)
            {
                var propertyName = property.GetPropertyAccess().Name;
                var dbItemsEntry = dbEntry.Collection(propertyName);
                var accessor = dbItemsEntry.Metadata.GetCollectionAccessor();

                await dbItemsEntry.LoadAsync();
                var dbItemsMap = ((IEnumerable<BaseEntity>)dbItemsEntry.CurrentValue)
                    .ToDictionary(e => e.Id);

                var items = (IEnumerable<BaseEntity>)accessor.GetOrCreate(entity, true);

                foreach (var item in items)
                {
                    if (!dbItemsMap.TryGetValue(item.Id, out var oldItem))
                        accessor.Add(dbEntity, item, true);
                    else
                    {
                        context.Entry(oldItem).CurrentValues.SetValues(item);
                        dbItemsMap.Remove(item.Id);
                    }
                }

                foreach (var oldItem in dbItemsMap.Values)
                    accessor.Remove(dbEntity, oldItem);
            }

            return await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            entities.Remove(entity);
            await context.SaveChangesAsync();
        }

        public async Task DeleteRangeAsync(IEnumerable<T> entityList)
        {
            if (entityList == null)
            {
                throw new ArgumentNullException(typeof(T).ToString());
            }
            entities.RemoveRange(entityList);
            await context.SaveChangesAsync();
        }

        //public async Task<int> SaveChangesAsync()
        //{
        //    return await context.SaveChangesAsync();
        //}

        //public async Task DisposeAsync()
        //{
        //    await context.DisposeAsync();
        //    GC.SuppressFinalize(this);
        //}
    }
}
