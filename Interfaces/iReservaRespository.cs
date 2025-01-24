using BusReservationMauiApp.Models;

namespace BusReservationMauiApp.Interfaces;

public interface iReservaRespository
{
    Task<bool> CreateReservaAsync(Reserva reserva);
    Task<bool> UpdateReservaAsync(Reserva reserva);
    Task<bool> DeleteReservaAsync(int reservaId);
    Task<IEnumerable<Reserva>> GetReservasByClienteAsync(int clienteId);

    Task<Reserva> GetReservaAsync(int reservaId);
}