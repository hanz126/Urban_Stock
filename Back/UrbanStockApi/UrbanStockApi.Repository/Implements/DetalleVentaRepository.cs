using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Repository.Implements
{
    public class DetalleVentaRepository : BaseRepository<DetalleVenta>, IDetalleVentaRepository
    {
        public DetalleVentaRepository(IDbConnection db) : base(db) { }
    }
}