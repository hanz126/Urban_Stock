using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UrbanStockApi.Models
{
    [Table("Cliente")]
    public class Cliente
    {
        [Key]
        public int ID_Cliente { get; set; }
        public string Documento { get; set; } = string.Empty;
        public string NombreCompleto { get; set; } = string.Empty;
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
    }
}
