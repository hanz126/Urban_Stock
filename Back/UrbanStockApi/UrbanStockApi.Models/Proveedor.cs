using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UrbanStockApi.Models
{
    [Table("Proveedor")]
    public class Proveedor
    {
        [Key]
        public int ID_Proveedor { get; set; }
        public string NombreEmpresa { get; set; } = string.Empty;
        public string? NombreContacto { get; set; }
        public string? Telefono { get; set; }
    }
}
