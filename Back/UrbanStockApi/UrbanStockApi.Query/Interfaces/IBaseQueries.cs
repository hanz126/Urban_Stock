using System.Collections.Generic;
using System.Threading.Tasks;

namespace UrbanStockApi.Query.Interfaces
{
    public interface IBaseQueries<T> where T : class
    {
        Task<IEnumerable<T>> ListarTodas();
        Task<T?> BuscarxId(int id);
    }
}