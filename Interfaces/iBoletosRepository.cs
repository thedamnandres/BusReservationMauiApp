using BusReservationMauiApp.Models;

namespace BusReservationMauiApp.Interfaces;

public interface iBoletosRepository
{
    Task<bool> CreateBoletoAsync(Boleto boleto);
    Task<bool> UpdateBoletoAsync(Boleto boleto);
    Task<bool> DeleteBoletoAsync(int boletoId);
    Task<IEnumerable<Boleto>> GetBoletosByReservaAsync(int reservaId);
    Task<Boleto> GetBoletoAsync(int boletoId);
}