using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;
using System;

namespace UrbanStockApi.Models
{
    [Table("Venta")]
    public class Venta
    {
        [Key]
        public int ID_Venta { get; set; }
        public int ID_Usuario { get; set; }
        public int? ID_Cliente { get; set; }
        public DateTime FechaHora { get; set; } = DateTime.Now;
        public decimal TotalVenta { get; set; }
    }
}