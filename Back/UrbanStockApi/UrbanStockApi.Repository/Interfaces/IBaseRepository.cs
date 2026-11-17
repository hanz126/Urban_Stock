using System.Threading.Tasks;

namespace UrbanStockApi.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> Add(T entity);
        Task<T> Update(T entity);
        Task<T?> Delete(int id);
    }
}