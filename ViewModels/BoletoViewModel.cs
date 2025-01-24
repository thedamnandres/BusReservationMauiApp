using System.Collections.ObjectModel;
using System.Windows.Input;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.Repositories;
using BusReservationMauiApp.Views.Boletos;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BusReservationMauiApp.ViewModels;

public partial class BoletoViewModel : ObservableObject
{
    private readonly BoletoRepository _boletoRepository;

    [ObservableProperty]
    private ObservableCollection<Boleto> boletos;

    public IAsyncRelayCommand RefreshBoletosCommand { get; }
    public IAsyncRelayCommand AddBoletoCommand { get; }

    public BoletoViewModel(string dbPath)
    {
        _boletoRepository = new BoletoRepository(dbPath);
        Boletos = new ObservableCollection<Boleto>();

        RefreshBoletosCommand = new AsyncRelayCommand(LoadBoletosAsync);
        AddBoletoCommand = new AsyncRelayCommand<Boleto>(AddBoletoAsync);

        _ = LoadBoletosAsync();
    }

    private async Task LoadBoletosAsync()
    {
        try
        {
            var boletosFromDb = await _boletoRepository.GetBoletosAsync();  // Llamar al método asincrónico
            Boletos.Clear();

            foreach (var boleto in boletosFromDb)
            {
                Boletos.Add(boleto);
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al cargar boletos: {ex.Message}", "OK");
        }
    }


    private async Task AddBoletoAsync(Boleto newBoleto)
    {
        try
        {
            var success = await _boletoRepository.CreateBoletoAsync(newBoleto);
            if (success)
            {
                Boletos.Add(newBoleto);
                await Application.Current.MainPage.DisplayAlert("Éxito", "Boleto agregado correctamente.", "OK");
            }
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error al agregar boleto: {ex.Message}", "OK");
        }
    }

}