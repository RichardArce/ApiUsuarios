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
    public class ProvinciaServicio : IProvinciaServicio
    {
        private readonly IMapper _mapper;
        private readonly IRepositorioGenerico<Provincia> _provinciaRepositorio;

        public ProvinciaServicio(IRepositorioGenerico<Provincia> provinciaRepositorio, IMapper mapper)
        {
            _provinciaRepositorio = provinciaRepositorio;
            _mapper = mapper;
        }

        public async Task<CustomResponse<ProvinciaDto>> AgregarProvinciaAsync(ProvinciaDto provinciaDto)
        {
            var respuesta = new CustomResponse<ProvinciaDto>();   //Obtener la información


            _provinciaRepositorio.AgregarAsync(_mapper.Map<Provincia>(provinciaDto));  //Mapeo y agrego el usuario

            //El repositorio me indica si pudo o no agregar el usuario
            if (!await _provinciaRepositorio.GuardarCambiosAsync())
            {
                respuesta.EsError = true;
                respuesta.Mensaje = "No se pudo agregar la provincia";
                return respuesta;
            }


            return respuesta;
        }

        public async Task<CustomResponse<ProvinciaDto>> ObtenerProvinciaPorIdAsync(int id)
        {
            var respuesta = new CustomResponse<ProvinciaDto>();

            var provincia = await _provinciaRepositorio.ObtenerPorIdAsync(id);

            respuesta.Data = _mapper.Map<ProvinciaDto>(provincia);
            return respuesta;

        }
    }
}
