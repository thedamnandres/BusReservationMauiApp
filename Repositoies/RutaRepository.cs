using BusReservationMauiApp.Interfaces;
using BusReservationMauiApp.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;

namespace BusReservationMauiApp.Repositoies;

public class RutaRepository : iRutaRepository
{
    private readonly string _filePath = Path.Combine(FileSystem.AppDataDirectory, "rutas.json");
    private readonly ObservableCollection<Ruta> _rutas;

    public RutaRepository()
    {
        _rutas = LoadRutasFromFile();
    }

    private ObservableCollection<Ruta> LoadRutasFromFile()
    {
        try
        {
            if (!File.Exists(_filePath))
            {
                var defaultRutas = new List<Ruta>
                {
                    new Ruta { IdRuta = 1, Origen = "Quito", Destino = "Ibarra", Duracion = TimeSpan.FromHours(2), Hora = TimeSpan.FromHours(10), Fecha = DateTime.Today },
                    new Ruta { IdRuta = 2, Origen = "Ambato", Destino = "Cuenca", Duracion = TimeSpan.FromHours(4), Hora = TimeSpan.FromHours(15), Fecha = DateTime.Today }
                };

                SaveRutasToFile(new ObservableCollection<Ruta>(defaultRutas));
                return new ObservableCollection<Ruta>(defaultRutas);
            }

            var json = File.ReadAllText(_filePath);
            var rutas = JsonConvert.DeserializeObject<List<Ruta>>(json) ?? new List<Ruta>();
            return new ObservableCollection<Ruta>(rutas);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al cargar las rutas: {ex.Message}");
            return new ObservableCollection<Ruta>(); // Colección vacía en caso de error
        }
    }

    private void SaveRutasToFile(ObservableCollection<Ruta> rutas)
    {
        try
        {
            var json = JsonConvert.SerializeObject(rutas, Formatting.Indented);
            File.WriteAllText(_filePath, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al guardar las rutas: {ex.Message}");
            throw;
        }
    }

    public Task<IEnumerable<Ruta>> GetAllRutasAsync()
    {
        return Task.FromResult(_rutas.AsEnumerable());
    }

    public Task<Ruta> GetRutaByIdAsync(int id)
    {
        var ruta = _rutas.FirstOrDefault(r => r.IdRuta == id);
        return Task.FromResult(ruta);
    }

    public Task AddRutaAsync(Ruta ruta)
    {
        try
        {
            _rutas.Add(ruta);
            SaveRutasToFile(_rutas);
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al agregar la ruta: {ex.Message}");
            throw;
        }
    }

    public Task UpdateRutaAsync(Ruta ruta)
    {
        try
        {
            var existingRuta = _rutas.FirstOrDefault(r => r.IdRuta == ruta.IdRuta);
            if (existingRuta != null)
            {
                existingRuta.Origen = ruta.Origen;
                existingRuta.Destino = ruta.Destino;
                existingRuta.Duracion = ruta.Duracion;
                existingRuta.Hora = ruta.Hora;
                existingRuta.Fecha = ruta.Fecha;
                SaveRutasToFile(_rutas);
            }
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al actualizar la ruta: {ex.Message}");
            throw;
        }
    }

    public Task DeleteRutaAsync(int id)
    {
        try
        {
            var ruta = _rutas.FirstOrDefault(r => r.IdRuta == id);
            if (ruta != null)
            {
                _rutas.Remove(ruta);
                SaveRutasToFile(_rutas);
            }
            return Task.CompletedTask;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error al eliminar la ruta: {ex.Message}");
            throw;
        }
    }
}