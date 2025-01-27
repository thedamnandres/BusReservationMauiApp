using Newtonsoft.Json;

namespace BusReservationMauiApp.Services;

public class BoletoService
{
    private readonly HttpClient _httpClient;

    public BoletoService()
    {
        _httpClient = new HttpClient { BaseAddress = new Uri("http://localhost:5191/api/Boletos/") };
    }

    // Método para obtener la lista de boletos desde la API
    public async Task<List<BoletoService>> ObtenerBoletos()
    {
        var response = await _httpClient.GetStringAsync("ver-boletos"); // Cambia la ruta si es necesario
        var boletos = JsonConvert.DeserializeObject<List<BoletoService>>(response); ////////// aqui cambiamos el json
        return boletos;
    }

    internal async Task<IEnumerable<object>> ObtenerBoletosAsync()
    {
        throw new NotImplementedException();
    }
}