using SQLite;
namespace BusReservationMauiApp.Models;


public class Usuario
{
    [PrimaryKey, AutoIncrement]
    public Guid Id { get; set; }
    public string Nombre { get; set; }

    public string contrasenia { get; set; }
    public string CI { get; set; }
    public string Correo { get; set; }
    public string Telefono { get; set; }

    public Usuario()
    {
        Id = Guid.NewGuid();
    }
}