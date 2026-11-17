using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class ProductoProveedorQueries : BaseQueries<ProductoProveedor>, IProductoProveedorQueries
    {
        public ProductoProveedorQueries(IDbConnection db) : base(db, "Producto_Proveedor", "ID_ProductoProveedor") { }
    }
}