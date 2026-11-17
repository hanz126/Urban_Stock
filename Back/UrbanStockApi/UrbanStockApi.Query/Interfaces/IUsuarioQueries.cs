using System.Threading.Tasks;
using UrbanStockApi.Models;

namespace UrbanStockApi.Query.Interfaces
{
    public interface IUsuarioQueries : IBaseQueries<Usuario>
    {
        Task<Usuario?> BuscarxCorreo(string correo);
    }
}