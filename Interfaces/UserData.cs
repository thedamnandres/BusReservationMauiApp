using BusReservationMauiApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusReservationMauiApp.Interfaces
{
    public class UserData
    {
        private SQLiteAsyncConnection _conexionBD;

        //INSTANCIA LOS DATOS DE LA BASE DE DATOS

        public UserData(SQLiteAsyncConnection conexionBD)
        {

            _conexionBD = conexionBD;


        }

        public Task<List<Usuario>> ListaUsuarios()
        {
            var lista = _conexionBD.Table<Usuario>().ToListAsync();
            return lista;
        }

        public Task<Usuario> ObtenerUsuario(string nombre, string contrasenia)
        {

            var usuario = _conexionBD.Table<Usuario>().Where(i => i.Nombre == nombre && i.contrasenia == contrasenia).FirstOrDefaultAsync();
            return usuario;
        }

        public Task<Usuario> ObtenerUsuario(Guid id)
        {

            var usuario = _conexionBD.Table<Usuario>().Where(i => i.Id == id).FirstOrDefaultAsync();
            return usuario;
        }
        public async Task<Usuario> ObtenerUltimoUsuario()
        {
            return await _conexionBD.Table<Usuario>().OrderByDescending(u => u.Id).FirstOrDefaultAsync();
        }


        public async Task<int> GuardarUsuario(Usuario user)
        {

            var usuarioGuardar = await ObtenerUsuario(user.Id);
            {
                if (usuarioGuardar == null)
                {
                    return await _conexionBD.InsertAsync(user);
                }
                else
                {
                    return await _conexionBD.UpdateAsync(user);
                }
            }
        }

        public async Task<int> EliminarUsuario(Guid id)
        {
            return await _conexionBD.DeleteAsync(id);
        }
    }



}

