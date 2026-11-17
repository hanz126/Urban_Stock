using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using UrbanStockApi.Query.Interfaces;

namespace UrbanStockApi.Query.Implements
{
    public class BaseQueries<T> : IBaseQueries<T> where T : class
    {
        protected readonly IDbConnection _db;
        private readonly string _tabla;
        private readonly string _columnaId;

        public BaseQueries(IDbConnection db, string tabla, string columnaId)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _tabla = tabla;
            _columnaId = columnaId;
        }

        public async Task<T?> BuscarxId(int id)
        {
            string sql = $"SELECT * FROM {_tabla} WHERE {_columnaId} = @id";
            return await _db.QueryFirstOrDefaultAsync<T>(sql, new { id });
        }

        public async Task<IEnumerable<T>> ListarTodas()
        {
            string sql = $"SELECT * FROM {_tabla}";
            return await _db.QueryAsync<T>(sql);
        }
    }
}