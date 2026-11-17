using System.Threading.Tasks;
using UrbanStockApi.Models;

namespace UrbanStockApi.Query.Interfaces
{
    public interface IClienteQueries : IBaseQueries<Cliente>
    {
        Task<Cliente?> BuscarxDocumento(string documento);
    }
}