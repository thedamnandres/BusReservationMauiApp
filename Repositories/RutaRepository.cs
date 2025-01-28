using BusReservationMauiApp.Interfaces;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.Services;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusReservationMauiApp.Repositories;

public class RutaRepository : iRutaRepository
{
    private readonly string _dbPath;
    private SQLiteConnection _connection;
    private readonly RutaService _rutaService;

    public RutaRepository(string dbPath)
    {
        _dbPath = dbPath;
        _rutaService = new RutaService();
        Init();
    }

    private void Init()
    {
        if (_connection == null)
        {
            _connection = new SQLiteConnection(_dbPath);
            _connection.CreateTable<Ruta>();
        }
    }

    public async Task<bool> CreateRutaAsync(Ruta ruta)
    {
        try
        {
            // Guardar localmente
            _connection.Insert(ruta);

            // Sincronizar con la API
            return await _rutaService.CreateRutaAsync(ruta);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateRutaAsync(Ruta ruta)
    {
        try
        {
            // Actualizar localmente
            _connection.Update(ruta);

            // Sincronizar con la API
            return await _rutaService.UpdateRutaAsync(ruta);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteRutaAsync(int id)
    {
        try
        {
            // Eliminar localmente
            _connection.Delete<Ruta>(id);

            // API
            return await _rutaService.DeleteRutaAsync(id);
        }
        catch
        {
            return false;
        }
    }

    public async Task<IEnumerable<Ruta>> GetAllRutasAsync()
    {
        try
        {
            // Obtener rutas desde la API
            var rutasRemotas = await _rutaService.GetAllRutasAsync();
            if (rutasRemotas.Any())
            {
                
                foreach (var ruta in rutasRemotas)
                {
                    var existe = _connection.Table<Ruta>().FirstOrDefault(r => r.IdRuta == ruta.IdRuta);
                    if (existe == null)
                    {
                        _connection.Insert(ruta);
                    }
                    else
                    {
                        _connection.Update(ruta);
                    }
                }
            }
        }
        catch
        {
            // Si la API falla, continuar con los datos locales
        }

        // Devolver datos locales
        return _connection.Table<Ruta>().ToList();
    }

    public async Task<Ruta> GetRutaAsync(int id)
    {
        try
        {
            // Ruta desde la API
            return await _rutaService.GetRutaAsync(id);
        }
        catch
        {
            // Si la API falla, obtener desde la base de datos local
            return _connection.Find<Ruta>(id);
        }
    }
}
