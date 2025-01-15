using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Collections.Generic;
using BusReservationMauiApp.Models;

namespace BusReservationMauiApp.Services
{
    public class RutaService
    {
        private readonly HttpClient _httpClient;

        public RutaService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:5191/api/")
            };
        }

        public async Task<IEnumerable<Ruta>> GetAllRutasAsync()
        {
            var response = await _httpClient.GetAsync("rutas");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<IEnumerable<Ruta>>() ?? new List<Ruta>();
            }
            return new List<Ruta>();
        }

        public async Task<Ruta> GetRutaAsync(int id)
        {
            return await _httpClient.GetFromJsonAsync<Ruta>($"rutas/{id}");
        }

        public async Task<bool> CreateRutaAsync(Ruta ruta)
        {
            var response = await _httpClient.PostAsJsonAsync("rutas", ruta);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateRutaAsync(Ruta ruta)
        {
            var response = await _httpClient.PutAsJsonAsync($"rutas/{ruta.IdRuta}", ruta);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteRutaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"rutas/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}