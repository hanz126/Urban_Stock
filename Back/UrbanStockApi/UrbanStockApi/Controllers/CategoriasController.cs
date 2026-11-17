using Microsoft.AspNetCore.Mvc;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriaQueries _queries;
        private readonly ICategoriaRepository _repo;

        public CategoriasController(ICategoriaQueries queries, ICategoriaRepository repo)
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
        public async Task<IActionResult> Create([FromBody] Categoria categoria)
        {
            var creada = await _repo.Add(categoria);
            return CreatedAtAction(nameof(GetById), new { id = creada.ID_Categoria }, creada);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Categoria categoria)
        {
            if (id != categoria.ID_Categoria) return BadRequest("El ID no coincide.");
            return Ok(await _repo.Update(categoria));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminada = await _repo.Delete(id);
            return eliminada == null ? NotFound() : Ok(eliminada);
        }
    }
}