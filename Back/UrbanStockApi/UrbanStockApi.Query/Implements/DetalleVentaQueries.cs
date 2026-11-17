using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class DetalleVentaQueries : BaseQueries<DetalleVenta>, IDetalleVentaQueries
    {
        public DetalleVentaQueries(IDbConnection db) : base(db, "DetalleVenta", "ID_Detalle") { }

        public async Task<IEnumerable<DetalleVenta>> ListarPorVenta(int idVenta)
        {
            string sql = "SELECT * FROM DetalleVenta WHERE ID_Venta = @idVenta";
            return await _db.QueryAsync<DetalleVenta>(sql, new { idVenta });
        }
    }
}