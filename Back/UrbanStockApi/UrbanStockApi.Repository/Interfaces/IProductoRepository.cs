using System.Threading.Tasks;
using UrbanStockApi.Models;

namespace UrbanStockApi.Repository.Interfaces
{
    public interface IProductoRepository : IBaseRepository<Producto>
    {
        Task<bool> ActualizarStock(int idProducto, int cantidadCambio);
    }
}