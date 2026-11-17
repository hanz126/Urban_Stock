using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UrbanStockApi.Models
{
    [Table("DetalleVenta")]
    public class DetalleVenta
    {
        [Key]
        public int ID_Detalle { get; set; }
        public int ID_Venta { get; set; }
        public int ID_Producto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
        public decimal Subtotal { get; set; }
    }
}