using System.Text.Json;
using BusReservationMauiApp.Models;

namespace BusReservationMauiApp.Repositories;

public class BoletoRepository
{
    private readonly HttpClient _httpClient;

    public BoletoRepository()
    {
        _httpClient = new HttpClient
        {
            BaseAddress = new Uri("http://localhost:5191/api/Boletos/") // Asegúrate de cambiar la URL base
        };
    }

    // Método para obtener los boletos desde la API
    public async Task<List<Boleto>> GetBoletosAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("ver-boleto"); // Endpoint para obtener boletos
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<List<Boleto>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }
            else
            {
                // Si la respuesta no es exitosa, puedes lanzar un error o manejarlo de otra forma
                throw new Exception($"Error al obtener los boletos: {response.StatusCode}");
            }
        }
        catch (Exception ex)
        {
            // Captura cualquier excepción que ocurra y muestra el mensaje en el depurador o en la UI
            System.Diagnostics.Debug.WriteLine($"Excepción: {ex.Message}");
            throw; // Relanzar la excepción para que puedas depurarla en el punto de llamada
        }
    }
}