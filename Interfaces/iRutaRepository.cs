using BusReservationMauiApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusReservationMauiApp.Interfaces;

public interface iRutaRepository
{
    Task<IEnumerable<Ruta>> GetAllRutasAsync();
    Task<Ruta> GetRutaByIdAsync(int id);
    Task AddRutaAsync(Ruta ruta);
    Task UpdateRutaAsync(Ruta ruta);
    Task DeleteRutaAsync(int id);
}