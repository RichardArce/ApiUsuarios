using ApiUsuarios.BLL.Dtos;
using ApiUsuarios.DLL.Entidades;
using ApiUsuarios.DLL.RepositorioGenerico;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.BLL.Servicios
{
    public class UsuarioServicio : IUsuariosServicio
    {
        //Inyección de dependencias
        //private readonly IUsuariosRepositorio _usuariosRepositorio;
        private readonly IMapper _mapper;
        private readonly IRepositorioGenerico<Usuario> _usuariosRepositorio;

        public UsuarioServicio(IRepositorioGenerico<Usuario> usuariosRepositorio, IMapper mapper)
        {
            _usuariosRepositorio = usuariosRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<UsuarioDto>> ActualizarUsuarioAsync(UsuarioDto usuarioDto) //CAPA DE SERVICIO EMPIEZA MÄS PROTAGONISMO
        {
            var respuesta = new CustomResponse<UsuarioDto>();
            var usuario = await _usuariosRepositorio.ObtenerPorIdAsync(usuarioDto.Id);  //Obtener informacion

            usuario.Nombre = usuarioDto.Nombre; //GERENCIAN, DEUDA TÉCNICA              //Se trabaja la informacion
            usuario.Apellido = usuarioDto.Apellido;
            usuario.Edad = usuarioDto.Edad;                                                         //validaciones

            _usuariosRepositorio.Actualizar(usuario);                                   //Se guarda/actualiza

            if (!await _usuariosRepositorio.GuardarCambiosAsync())                      //se confirma
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pudo actualizar el usuario";
                return respuesta;
            }
            return respuesta;
        }
        public async Task<CustomResponse<UsuarioDto>> EliminarUsuarioAsync(int id)
        {
            var respuesta = new CustomResponse<UsuarioDto>();

            var usuario = await _usuariosRepositorio.ObtenerPorIdAsync(id);

            _usuariosRepositorio.Eliminar(usuario);

            if (!await _usuariosRepositorio.GuardarCambiosAsync())
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pudo eliminar el usuario";
                return respuesta;
            }

            return respuesta;

        }
        public async Task<CustomResponse<UsuarioDto>> AgregarUsuarioAsync(UsuarioDto usuarioDto)
        {
            var respuesta = new CustomResponse<UsuarioDto>();   //Obtener la información

            //validaciones de negocio
            if (usuarioDto.Edad > 65)
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pueden agregar usuarios mayores de 65 años";
                return respuesta;
            }

            _usuariosRepositorio.AgregarAsync(_mapper.Map<Usuario>(usuarioDto));  //Mapeo y agrego el usuario

            //El repositorio me indica si pudo o no agregar el usuario
            if (!await _usuariosRepositorio.GuardarCambiosAsync())
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pudo agregar el usuario";
                return respuesta;
            }


            return respuesta;
        }
        public async Task<CustomResponse<UsuarioDto>> ObtenerUsuarioPorIdAsync(int id)
        {
            var respuesta = new CustomResponse<UsuarioDto>();

            var usuario = await _usuariosRepositorio.ObtenerPorIdAsync(id);

            respuesta.Data = _mapper.Map<UsuarioDto>(usuario);
            return respuesta;

        }
        public async Task<CustomResponse<List<UsuarioDto>>> ObtenerUsuariosAsync()
        {
            var respuesta = new CustomResponse<List<UsuarioDto>>();

            var usuarios = await _usuariosRepositorio.ObtenerTodosAsync();

            var usuariosDto = _mapper.Map<List<UsuarioDto>>(usuarios);
            respuesta.Data = usuariosDto;
            return respuesta;
        }
    }
}
