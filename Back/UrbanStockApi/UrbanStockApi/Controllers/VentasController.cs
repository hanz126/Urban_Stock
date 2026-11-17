using Microsoft.AspNetCore.Mvc;
using UrbanStockApi.Models;
using UrbanStockApi.Query.Interfaces;
using UrbanStockApi.Repository.Interfaces;

namespace UrbanStockApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VentasController : ControllerBase
    {
        private readonly IVentaQueries _ventaQueries;
        private readonly IDetalleVentaQueries _detalleQueries;
        private readonly IVentaRepository _ventaRepo;
        private readonly IDetalleVentaRepository _detalleRepo;
        private readonly IProductoRepository _productoRepo;

        public VentasController(
            IVentaQueries ventaQueries,
            IDetalleVentaQueries detalleQueries,
            IVentaRepository ventaRepo,
            IDetalleVentaRepository detalleRepo,
            IProductoRepository productoRepo)
        {
            _ventaQueries = ventaQueries;
            _detalleQueries = detalleQueries;
            _ventaRepo = ventaRepo;
            _detalleRepo = detalleRepo;
            _productoRepo = productoRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _ventaQueries.ListarTodas());

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var venta = await _ventaQueries.BuscarxId(id);
            if (venta == null) return NotFound();

            var detalles = await _detalleQueries.ListarPorVenta(id);
            return Ok(new { Venta = venta, Detalles = detalles });
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta([FromBody] RegistrarVentaRequest request)
        {
            if (request.Detalles == null || !request.Detalles.Any())
                return BadRequest("La venta debe tener al menos un producto.");

            // 1. Calcular total y guardar cabecera Venta
            request.Venta.FechaHora = DateTime.Now;
            request.Venta.TotalVenta = request.Detalles.Sum(d => d.Cantidad * d.PrecioUnitario);
            var ventaCreada = await _ventaRepo.Add(request.Venta);

            // 2. Guardar cada DetalleVenta y descontar Stock del Producto
            foreach (var detalle in request.Detalles)
            {
                detalle.ID_Venta = ventaCreada.ID_Venta;
                detalle.Subtotal = detalle.Cantidad * detalle.PrecioUnitario;
                await _detalleRepo.Add(detalle);
                await _productoRepo.ActualizarStock(detalle.ID_Producto, -detalle.Cantidad);
            }

            return CreatedAtAction(nameof(GetById), new { id = ventaCreada.ID_Venta }, ventaCreada);
        }
    }

    public class RegistrarVentaRequest
    {
        public Venta Venta { get; set; } = new();
        public List<DetalleVenta> Detalles { get; set; } = new();
    }
}