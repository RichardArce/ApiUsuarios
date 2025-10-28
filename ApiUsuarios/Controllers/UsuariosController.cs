using ApiUsuarios.BLL.Dtos;
using ApiUsuarios.BLL.Servicios;
using Microsoft.AspNetCore.Mvc;

namespace ApiUsuarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : Controller
    {


        private readonly IUsuariosServicio _usuariosServicio;

        public UsuariosController(IUsuariosServicio usuariosServicio)
        {
            _usuariosServicio = usuariosServicio;
        }   

        [HttpGet(Name ="ObtenerUsuarios")] //ObtenerTodos
        public async Task<IActionResult> ObtenerUsuarios()
        {
           var respuesta = await _usuariosServicio.ObtenerUsuariosAsync();
           return Ok(respuesta);
        }

        [HttpPost(Name = "CrearUsuario")] //Crear
        public async Task<IActionResult> CrearUsuario(UsuarioDto user)
        {
            var respuesta = await _usuariosServicio.AgregarUsuarioAsync(user);
            
            if(respuesta.EsError)
            {
                return BadRequest(respuesta.Mensaje);
            }

            return Ok(respuesta);

        }

        [HttpGet("{id}",Name = "ObtenerUsuario")]
        public async Task<IActionResult> ObtenerUsuario(int id)
        {
            var respuesta = await _usuariosServicio.ObtenerUsuarioPorIdAsync(id);

            if (respuesta.Data is null)
            {
                return NotFound("Usuario no encontrado"); //Mensaje que pudo haber estado en la capa de negocio
            }

            return Ok(respuesta);

        }

        [HttpDelete("{id}", Name = "EliminarUsuario")]
        public async Task<IActionResult> EliminarUsuario(int id)
        {
            var respuesta = await _usuariosServicio.EliminarUsuarioAsync(id);

            return Ok(respuesta);

        }

        [HttpPut(Name = "ActualizarUsuario")]
        public async Task<IActionResult> ActualizarUsuario(UsuarioDto user)
        {
            var respuesta = await _usuariosServicio.ActualizarUsuarioAsync(user);
            return Ok(respuesta);

        }
    }
}
