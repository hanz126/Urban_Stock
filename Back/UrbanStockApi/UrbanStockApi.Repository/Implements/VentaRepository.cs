using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Repository.Implements
{
    public class VentaRepository : BaseRepository<Venta>, IVentaRepository
    {
        public VentaRepository(IDbConnection db) : base(db) { }
    }
}