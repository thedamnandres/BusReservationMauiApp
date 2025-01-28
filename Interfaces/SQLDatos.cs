using BusReservationMauiApp.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusReservationMauiApp.Interfaces
{
    public class SQLDatos
    {
        readonly SQLiteAsyncConnection _conexionBD;

        public UserData UserDataTabla { get; set; }

        public SQLDatos(string path)
        {
            _conexionBD = new SQLiteAsyncConnection(path);

            _conexionBD.CreateTableAsync<Usuario>().Wait();

            UserDataTabla = new UserData(_conexionBD);

        }


    }


}

