using ApiUsuarios.DLL.Entidades;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiUsuarios.DLL.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly ApiContext _context;

        public UsuariosRepositorio(ApiContext context)
        {
            _context = context;
        }

        /*****COPY PASTE****/
        public async Task<bool> ActualizarUsuarioAsync(Usuario usuario)
        {
            var usuarioActualizar = _context.Usuarios.FirstOrDefault(u => u.Id == usuario.Id); //recupera el objeto

            usuarioActualizar.Nombre = usuario.Nombre;              //actualiza la información
            usuarioActualizar.Apellido = usuario.Apellido;
            usuarioActualizar.Edad = usuario.Edad;

            var result = await _context.SaveChangesAsync();     //Aplica los cambios

            return result > 0; //check si funciono
        }

        public async Task<bool> AgregarUsuarioAsync(Usuario usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EliminarUsuarioAsync(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            _context.Usuarios.Remove(usuario);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<Usuario> ObtenerUsuarioPorIdAsync(int id)
        {
            //SP //API // ETC
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            return usuario;
        }

        public async Task<List<Usuario>> ObtenerUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync(); //
        }
    }
}
