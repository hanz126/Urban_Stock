using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class VentaQueries : BaseQueries<Venta>, IVentaQueries
    {
        public VentaQueries(IDbConnection db) : base(db, "Venta", "ID_Venta") { }
    }
}