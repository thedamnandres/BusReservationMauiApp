using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusReservationMauiApp.Interfaces
{
    internal interface IUsuario
    {
        string SQLiteLocalPath(string bancoDatos);
    }
}
