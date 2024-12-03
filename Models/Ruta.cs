namespace BusReservationMauiApp.Models;

public class Ruta
{
    public int IdRuta { get; set; }
    public string Origen { get; set; }
    public string Destino { get; set; }
    public TimeSpan Duracion { get; set; }
    public TimeSpan Hora { get; set; }
    public DateTime Fecha { get; set; }
}