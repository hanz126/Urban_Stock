using System.Data;
using UrbanStockApi.Models;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Repository.Implements
{
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(IDbConnection db) : base(db) { }
    }
}