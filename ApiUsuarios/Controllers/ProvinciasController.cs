using ApiUsuarios.BLL.Dtos;
using ApiUsuarios.BLL.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProvinciasController : Controller
    {
        private readonly IProvinciaServicio _provinciaServicio;

        public ProvinciasController(IProvinciaServicio provinciaServicio)
        {
            _provinciaServicio = provinciaServicio;
        }

        [HttpGet("{id}", Name = "ObtenerProvincia")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var respuesta = await _provinciaServicio.ObtenerProvinciaPorIdAsync(id);

            if (respuesta.Data is null)
            {
                return NotFound("Provincia no encontrada"); //Mensaje que pudo haber estado en la capa de negocio
            }

            return Ok(respuesta);

        }
        [HttpPost(Name = "CrearProvincia")] //Crear
        public async Task<IActionResult> CrearProvincia(ProvinciaDto user)
        {
            var respuesta = await _provinciaServicio.AgregarProvinciaAsync(user);

            if (respuesta.EsError)
            {
                return BadRequest(respuesta.Mensaje);
            }

            return Ok(respuesta);

        }
    }
}
