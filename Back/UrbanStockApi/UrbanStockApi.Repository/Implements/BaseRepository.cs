using Dapper.Contrib.Extensions;
using System;
using System.Data;
using System.Threading.Tasks;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Repository.Implements
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        protected readonly IDbConnection _db;

        public BaseRepository(IDbConnection db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<T> Add(T entity)
        {
            await _db.InsertAsync(entity);
            return entity;
        }

        public async Task<T?> Delete(int id)
        {
            var entity = await _db.GetAsync<T>(id);
            if (entity != null)
            {
                await _db.DeleteAsync(entity);
            }
            return entity;
        }

        public async Task<T> Update(T entity)
        {
            await _db.UpdateAsync(entity);
            return entity;
        }
    }
}