using Microsoft.AspNetCore.Mvc;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientesController : ControllerBase
    {
        private readonly IClienteQueries _queries;
        private readonly IClienteRepository _repo;

        public ClientesController(IClienteQueries queries, IClienteRepository repo)
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

        [HttpGet("documento/{documento}")]
        public async Task<IActionResult> GetByDocumento(string documento)
        {
            var item = await _queries.BuscarxDocumento(documento);
            return item == null ? NotFound() : Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Cliente cliente)
        {
            var creado = await _repo.Add(cliente);
            return CreatedAtAction(nameof(GetById), new { id = creado.ID_Cliente }, creado);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Cliente cliente)
        {
            if (id != cliente.ID_Cliente) return BadRequest("El ID no coincide.");
            return Ok(await _repo.Update(cliente));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var eliminado = await _repo.Delete(id);
            return eliminado == null ? NotFound() : Ok(eliminado);
        }
    }
}