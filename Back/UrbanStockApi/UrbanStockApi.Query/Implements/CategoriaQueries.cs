using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class CategoriaQueries : BaseQueries<Categoria>, ICategoriaQueries
    {
        public CategoriaQueries(IDbConnection db) : base(db, "Categoria", "ID_Categoria") { }
    }
}