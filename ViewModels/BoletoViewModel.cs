using System.Collections.ObjectModel;
using System.Text.Json;
using System.Windows.Input;
using BusReservationMauiApp.Models;

namespace BusReservationMauiApp.ViewModels;

public class BoletoViewModel : BindableObject
{
    private ObservableCollection<Boleto> _boletos;

    public ObservableCollection<Boleto> Boletos
    {
        get => _boletos;
        set
        {
            _boletos = value;
            OnPropertyChanged();
        }
    }

    private Boleto _newBoleto;
    private object reservaId;

    public Boleto NewBoleto
    {
        get => _newBoleto;
        set
        {
            _newBoleto = value;
            OnPropertyChanged();
        }
    }

    public ICommand CrearBoletoCommand { get; }
    public ICommand ActualizarBoletoCommand { get; }
    public ICommand EliminarBoletoCommand { get; }

    public BoletoViewModel()
    {
        Boletos = new ObservableCollection<Boleto>();
        NewBoleto = new Boleto(); // Inicializar NewBoleto para que pueda ser usado en la UI
    }

    // Cargar todos los boletos desde la API
    public async Task CargarBoletos()
    {
        try
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("http://localhost:5191/api/Boleto/ver-boletos");
            var boletos = JsonSerializer.Deserialize<List<Boleto>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Boletos.Clear();
            if (boletos != null)
            {
                foreach (var boleto in boletos)
                {
                    Boletos.Add(boleto);
                }
            }
        }
        catch (HttpRequestException ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", $"Error de conexión: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await Application.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
        }
    }
}