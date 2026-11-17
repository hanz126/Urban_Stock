using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Repository.Implements
{
    public class ProductoProveedorRepository : BaseRepository<ProductoProveedor>, IProductoProveedorRepository
    {
        public ProductoProveedorRepository(IDbConnection db) : base(db) { }
    }
}