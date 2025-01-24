using System.Collections.ObjectModel;
using System.Threading.Tasks;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.Repositories;
using BusReservationMauiApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BusReservationMauiApp.ViewModels;

public partial class ReservaViewModel : ObservableObject
{
    private readonly ReservaRepository _reservaRepository;

    [ObservableProperty]
    private ObservableCollection<Reserva> reservas;

    public IAsyncRelayCommand RefreshReservasCommand { get; }
    public IAsyncRelayCommand AddReservaCommand { get; }

    public ReservaViewModel()
    {
        string dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "reservas.db");
        _reservaRepository = new ReservaRepository(dbPath);
        Reservas = new ObservableCollection<Reserva>();

        RefreshReservasCommand = new AsyncRelayCommand(LoadReservasAsync);
        AddReservaCommand = new AsyncRelayCommand<Reserva>(AddReservaAsync);

        _ = LoadReservasAsync();
    }

    private async Task LoadReservasAsync()
    {
        try
        {
            var reservasFromDb = await _reservaRepository.GetReservasAsync();
            Reservas.Clear();

            foreach (var reserva in reservasFromDb)
            {
                Reservas.Add(reserva);
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar reservas: {ex.Message}", "OK");
        }
    }

    private async Task AddReservaAsync(Reserva newReserva)
    {
        try
        {
            var success = await _reservaRepository.CreateReservaAsync(newReserva);
            if (success)
            {
                Reservas.Add(newReserva);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Reserva agregada correctamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al agregar reserva: {ex.Message}", "OK");
        }
    }
}