using Dapper;
using System.Data;
using System.Threading.Tasks;
using UrbanStockApi.Models;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Repository.Implements
{
    public class ProductoRepository : BaseRepository<Producto>, IProductoRepository
    {
        public ProductoRepository(IDbConnection db) : base(db) { }

        // Usa cantidad positiva para entradas (+5) o negativa para ventas (-2)
        public async Task<bool> ActualizarStock(int idProducto, int cantidadCambio)
        {
            string sql = "UPDATE Producto SET Stock = Stock + @cantidadCambio WHERE ID_Producto = @idProducto";
            int filas = await _db.ExecuteAsync(sql, new { idProducto, cantidadCambio });
            return filas > 0;
        }
    }
}