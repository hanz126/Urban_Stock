using System.Collections.Generic;
using System.Threading.Tasks;
using UrbanStockApi.Models;

namespace UrbanStockApi.Query.Interfaces
{
    public interface IDetalleVentaQueries : IBaseQueries<DetalleVenta>
    {
        Task<IEnumerable<DetalleVenta>> ListarPorVenta(int idVenta);
    }
}