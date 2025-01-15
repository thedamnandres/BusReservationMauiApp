using BusReservationMauiApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusReservationMauiApp.Interfaces;

public interface iRutaRepository
{
    Task<bool> CreateRutaAsync(Ruta ruta);
    Task<bool> UpdateRutaAsync(Ruta ruta); 
    Task<bool> DeleteRutaAsync(int id); 
    Task<IEnumerable<Ruta>> GetAllRutasAsync();
    Task<Ruta> GetRutaAsync(int id); 
}