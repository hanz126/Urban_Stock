using Dapper;
using System.Data;
using System.Threading.Tasks;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class ClienteQueries : BaseQueries<Cliente>, IClienteQueries
    {
        public ClienteQueries(IDbConnection db) : base(db, "Cliente", "ID_Cliente") { }

        public async Task<Cliente?> BuscarxDocumento(string documento)
        {
            string sql = "SELECT * FROM Cliente WHERE Documento = @documento";
            return await _db.QueryFirstOrDefaultAsync<Cliente>(sql, new { documento });
        }
    }
}