using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Dapper.Contrib.Extensions;

namespace UrbanStockApi.Models
{
    [Table("Usuario")]
    public class Usuario
    {
        [Key]
        public int ID_Usuario { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string ContrasenaHash { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty; // 'Admin' o 'Vendedor'
        public bool Estado { get; set; } = true;
    }
}
