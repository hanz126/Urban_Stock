using Dapper;
using System.Data;
using System.Threading.Tasks;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class UsuarioQueries : BaseQueries<Usuario>, IUsuarioQueries
    {
        public UsuarioQueries(IDbConnection db) : base(db, "Usuario", "ID_Usuario") { }

        public async Task<Usuario?> BuscarxCorreo(string correo)
        {
            string sql = "SELECT * FROM Usuario WHERE Correo = @correo";
            return await _db.QueryFirstOrDefaultAsync<Usuario>(sql, new { correo });
        }
    }
}