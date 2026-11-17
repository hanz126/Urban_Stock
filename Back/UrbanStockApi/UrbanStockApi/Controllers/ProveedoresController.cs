using Microsoft.AspNetCore.Mvc;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProveedoresController : ControllerBase
    {
        private readonly IProveedorQueries _queries;
        private readonly IProveedorRepository _repo;

        public ProveedoresController(IProveedorQueries queries, IProveedorRepository repo)
        {
            _queries = queries;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _queries.ListarTodas());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var item = await _queries.BuscarxId(id);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Proveedor proveedor)
        {
            var creado = await _repo.Add(proveedor);
            return CreatedAtAction(nameof(GetById), new { id = creado.ID_Proveedor }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Proveedor proveedor)
        {
            if (id != proveedor.ID_Proveedor) return BadRequest("El ID no coincide.");
            return Ok(await _repo.Update(proveedor));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _repo.Delete(id);
            return eliminado == null ? NotFound() : Ok(eliminado);
        }
    }
}