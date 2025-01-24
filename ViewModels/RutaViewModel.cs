using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BusReservationMauiApp.ViewModels;

public partial class RutaViewModel : ObservableObject
{
    private readonly RutaService _rutaService;

    [ObservableProperty]
    private ObservableCollection<Ruta> rutas;

    [ObservableProperty]
    private Ruta nuevaRuta;

    public IAsyncRelayCommand RefreshRutasCommand { get; }
    public IAsyncRelayCommand CrearRutaCommand { get; }
    public IAsyncRelayCommand<Ruta> UpdateRutaCommand { get; }
    public IAsyncRelayCommand<int> DeleteRutaCommand { get; }

    // Default constructor
    public RutaViewModel() : this(new RutaService())
    {
    }

    public RutaViewModel(RutaService rutaService)
    {
        _rutaService = rutaService;
        Rutas = new ObservableCollection<Ruta>();
        NuevaRuta = new Ruta();

        RefreshRutasCommand = new AsyncRelayCommand(LoadRutasAsync);
        CrearRutaCommand = new AsyncRelayCommand(CreateRutaAsync);
        UpdateRutaCommand = new AsyncRelayCommand<Ruta>(UpdateRutaAsync);
        DeleteRutaCommand = new AsyncRelayCommand<int>(DeleteRutaAsync);

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

    private async Task CreateRutaAsync()
    {
        try
        {
            var success = await _rutaService.CreateRutaAsync(NuevaRuta);
            if (success)
            {
                await LoadRutasAsync();
                NuevaRuta = new Ruta();
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al crear ruta: {ex.Message}", "OK");
        }
    }

    private async Task UpdateRutaAsync(Ruta ruta)
    {
        try
        {
            var success = await _rutaService.UpdateRutaAsync(ruta);
            if (success)
            {
                await LoadRutasAsync();
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al actualizar ruta: {ex.Message}", "OK");
        }
    }

    private async Task DeleteRutaAsync(int rutaId)
    {
        try
        {
            var success = await _rutaService.DeleteRutaAsync(rutaId);
            if (success)
            {
                await LoadRutasAsync();
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al eliminar ruta: {ex.Message}", "OK");
        }
    }
}