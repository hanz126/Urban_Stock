using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanStockApi.Models;

namespace UrbanStockApi.Query.Interfaces
{
    public interface IProductoQueries : IBaseQueries<Producto>
    {
        Task<IEnumerable<Producto>> ListarStockBajo();
    }
}