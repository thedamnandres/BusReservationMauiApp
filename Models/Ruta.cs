using System.ComponentModel.DataAnnotations;
using SQLite;

namespace BusReservationMauiApp.Models;

public class Ruta
{
    public Ruta()
    {
        Origen = string.Empty;
        Destino = string.Empty;
    }

    [PrimaryKey, AutoIncrement]
    public int IdRuta { get; set; }

    public string Origen { get; set; }

    public string Destino { get; set; }

    public TimeSpan Duracion { get; set; }

    public DateTime FechaSalida { get; set; }

    public TimeSpan Hora { get; set; }
}