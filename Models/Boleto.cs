namespace BusReservationMauiApp.Models;

public class Boleto
{
    public UriImageSource QRCode;
    public int Id { get; set; } // Coincide con "id" en el JSON
    public int ReservaId { get; set; } // Coincide con "reservaId"
    public string NombrePasajero { get; set; } // Coincide con "nombrePasajero"
    public string Asiento { get; set; } // Coincide con "asiento"
    public DateTime FechaViaje { get; set; } // Coincide con "fechaViaje"
    public decimal Precio { get; set; } // Coincide con "precio"
    public string EstadoReserva { get; set; } // Coincide con "estadoReserva"
    public DateTime FechaReserva { get; internal set; }
    public string CodigoQR { get; set; } // Aquí se almacenará la URL del QR
}