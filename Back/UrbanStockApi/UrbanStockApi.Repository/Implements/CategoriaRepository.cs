using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Repository.Implements
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(IDbConnection db) : base(db) { }
    }
}