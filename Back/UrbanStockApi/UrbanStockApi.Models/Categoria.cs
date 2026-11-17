using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper.Contrib.Extensions;

namespace UrbanStockApi.Models
{
    [Table("Categoria")]
    public class Categoria
    {
        [Key]
        public int ID_Categoria { get; set; }
        public string NombreCategoria { get; set; } = string.Empty;
    }
}
