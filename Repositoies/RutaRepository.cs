using BusReservationMauiApp.Interfaces;
using BusReservationMauiApp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BusReservationMauiApp.Repositoies;

public class RutaRepository : iRutaRepository
{
    private readonly ObservableCollection<Ruta> _rutas;

    public RutaRepository()
    {
        _rutas = new ObservableCollection<Ruta>
        {
            new Ruta { IdRuta = 1, Origen = "Ciudad A", Destino = "Ciudad B", Duracion = TimeSpan.FromHours(2), Hora = TimeSpan.FromHours(10), Fecha = DateTime.Today },
            new Ruta { IdRuta = 2, Origen = "Ciudad C", Destino = "Ciudad D", Duracion = TimeSpan.FromHours(3), Hora = TimeSpan.FromHours(15), Fecha = DateTime.Today }
        };
    }

    public Task<IEnumerable<Ruta>> GetAllRutasAsync()
    {
        return Task.FromResult(_rutas.AsEnumerable());
    }

    public Task<Ruta> GetRutaByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddRutaAsync(Ruta ruta)
    {
        _rutas.Add(ruta);
        return Task.CompletedTask;
    }

    public Task UpdateRutaAsync(Ruta ruta)
    {
        var existingRuta = _rutas.FirstOrDefault(r => r.IdRuta == ruta.IdRuta);
        if (existingRuta != null)
        {
            existingRuta.Origen = ruta.Origen;
            existingRuta.Destino = ruta.Destino;
            existingRuta.Duracion = ruta.Duracion;
            existingRuta.Hora = ruta.Hora;
            existingRuta.Fecha = ruta.Fecha;
        }
        return Task.CompletedTask;
    }

    public Task DeleteRutaAsync(int id)
    {
        var ruta = _rutas.FirstOrDefault(r => r.IdRuta == id);
        if (ruta != null)
        {
            _rutas.Remove(ruta);
        }
        return Task.CompletedTask;
    }
}