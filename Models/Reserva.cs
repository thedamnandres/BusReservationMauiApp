using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace BusReservationMauiApp.Models
{
    public class Reserva
    {
        [PrimaryKey, AutoIncrement]
        public int ReservaId { get; set; }

        public int ClienteId { get; set; }

        public int RutaId { get; set; }
        [Ignore]
        public Ruta Ruta { get; set; }

        public DateTime FechaReserva { get; set; }

        public string Asiento { get; set; }

        public string EstadoReserva { get; set; }

        public float Precio { get; set; }

        [Ignore]
        public List<Boleto> Boletos { get; set; } = new List<Boleto>();
        
        public Color ColorEstadoReserva => EstadoReserva == "A TIEMPO" ? Colors.Green : Colors.Red;

    }
}
