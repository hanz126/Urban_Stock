using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class ProveedorQueries : BaseQueries<Proveedor>, IProveedorQueries
    {
        public ProveedorQueries(IDbConnection db) : base(db, "Proveedor", "ID_Proveedor") { }
    }
}