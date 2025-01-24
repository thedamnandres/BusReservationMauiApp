using SQLite;

namespace BusReservationMauiApp.Models;

public class Boleto
{
    [PrimaryKey, AutoIncrement]
    public int BoletoId { get; set; }

    // Foreign Key a Ruta
    [Indexed]
    public int RutaId { get; set; }

    // Foreign Key a Reserva
    [Indexed]
    public int ReservaId { get; set; }

    public decimal Precio { get; set; }
    public bool EstadoReserva { get; set; } // Indica si el boleto fue reservado
}