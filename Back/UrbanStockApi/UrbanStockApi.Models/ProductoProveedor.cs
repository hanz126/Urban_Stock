using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UrbanStockApi.Models
{
    [Table("Producto_Proveedor")]
    public class ProductoProveedor
    {
        [Key]
        public int ID_ProductoProveedor { get; set; }
        public int ID_Producto { get; set; }
        public int ID_Proveedor { get; set; }
    }
}