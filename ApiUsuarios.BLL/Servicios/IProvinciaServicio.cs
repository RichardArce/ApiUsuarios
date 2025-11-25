using ApiUsuarios.BLL.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.BLL.Servicios
{
    public interface IProvinciaServicio
    {
        Task<CustomResponse<ProvinciaDto>> ObtenerProvinciaPorIdAsync(int id);

        Task<CustomResponse<ProvinciaDto>> AgregarProvinciaAsync(ProvinciaDto usuarioDto);
    }
}
