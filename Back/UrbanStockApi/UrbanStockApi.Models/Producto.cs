using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UrbanStockApi.Models
{
    [Table("Producto")]
    public class Producto
    {
        [Key]
        public int ID_Producto { get; set; }
        public int ID_Categoria { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Talla { get; set; } = string.Empty;
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Stock { get; set; }
        public int StockMinimo { get; set; } = 3;

        [Computed]
        public bool AlertaStockBajo => Stock <= StockMinimo;
    }
}
