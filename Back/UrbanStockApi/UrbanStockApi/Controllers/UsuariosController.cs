using Microsoft.AspNetCore.Mvc;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioQueries _queries;
        private readonly IUsuarioRepository _repo;

        public UsuariosController(IUsuarioQueries queries, IUsuarioRepository repo)
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
        public async Task<IActionResult> Create([FromBody] Usuario usuario)
        {
            var creado = await _repo.Add(usuario);
            return CreatedAtAction(nameof(GetById), new { id = creado.ID_Usuario }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Usuario usuario)
        {
            if (id != usuario.ID_Usuario) return BadRequest("El ID no coincide.");
            return Ok(await _repo.Update(usuario));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _repo.Delete(id);
            return eliminado == null ? NotFound() : Ok(eliminado);
        }
    }
}