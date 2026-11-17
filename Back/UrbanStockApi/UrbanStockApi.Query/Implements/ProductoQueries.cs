using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class ProductoQueries : BaseQueries<Producto>, IProductoQueries
    {
        public ProductoQueries(IDbConnection db) : base(db, "Producto", "ID_Producto") { }

        public async Task<IEnumerable<Producto>> ListarStockBajo()
        {
            string sql = "SELECT * FROM Producto WHERE Stock <= StockMinimo";
            return await _db.QueryAsync<Producto>(sql);
        }
    }
}