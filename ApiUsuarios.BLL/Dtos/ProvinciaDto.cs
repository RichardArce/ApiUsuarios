using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.BLL.Dtos
{
    public class ProvinciaDto
    {
        public int Id { get; set; } // Autoincremental por convención
        public string Nombre { get; set; } = string.Empty;
    }
}
