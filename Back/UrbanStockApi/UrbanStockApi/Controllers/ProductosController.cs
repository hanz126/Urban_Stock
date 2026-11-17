using Microsoft.AspNetCore.Mvc;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IProductoQueries _queries;
        private readonly IProductoRepository _repo;

        public ProductosController(IProductoQueries queries, IProductoRepository repo)
        {
            _queries = queries;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _queries.ListarTodas());

        [HttpGet("stock-bajo")]
        public async Task<IActionResult> GetStockBajo() => Ok(await _queries.ListarStockBajo());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _queries.BuscarxId(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Producto producto)
        {
            var creado = await _repo.Add(producto);
            return CreatedAtAction(nameof(GetById), new { id = creado.ID_Producto }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Producto producto)
        {
            if (id != producto.ID_Producto) return BadRequest("El ID no coincide.");
            return Ok(await _repo.Update(producto));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _repo.Delete(id);
            return eliminado == null ? NotFound() : Ok(eliminado);
        }
    }
}