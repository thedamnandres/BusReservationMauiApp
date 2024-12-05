using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusReservationMauiApp.Models
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public int ClienteId { get; set; }
        public int RutaId { get; set; }

        public DateTime FechaReserva { get; set; }
        public string Asiento { get; set; }
        public string EstadoReserva { get; set; }
        public float Precio { get; set; }
    }
}
