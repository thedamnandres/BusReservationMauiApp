using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BusReservationMauiApp.ViewModels
{
    public partial class RutaViewModel : ObservableObject
    {
        private readonly RutaService _rutaService;

        [ObservableProperty]
        private ObservableCollection<Ruta> rutas;

        public IAsyncRelayCommand RefreshRutasCommand { get; }

        public RutaViewModel(RutaService rutaService)
        {
            _rutaService = rutaService;
            Rutas = new ObservableCollection<Ruta>();

            RefreshRutasCommand = new AsyncRelayCommand(LoadRutasAsync);

            // Cargar rutas al inicializar
            _ = LoadRutasAsync();
        }

        private async Task LoadRutasAsync()
        {
            try
            {
                var rutasFromApi = await _rutaService.GetAllRutasAsync();
                Rutas.Clear();

                foreach (var ruta in rutasFromApi)
                {
                    Rutas.Add(ruta);
                }
            }
            catch (Exception ex)
            {
                await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar rutas: {ex.Message}", "OK");
            }
        }
    }
}