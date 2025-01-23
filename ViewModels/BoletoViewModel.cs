using System.Collections.ObjectModel;
using System.Windows.Input;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.Views.Boletos;

namespace BusReservationMauiApp.ViewModels;

public class BoletoViewModel : BindableObject
{
    public ObservableCollection<Reserva> Reservas { get; set; }
    public ICommand MostrarQRCommand { get; }

    public BoletoViewModel()
    {
        Reservas = new ObservableCollection<Reserva>
        {
            new Reserva { ReservaId = 1, Asiento = "A1", FechaReserva = DateTime.Today, EstadoReserva = "A TIEMPO", Precio = 8f },
            new Reserva { ReservaId = 2, Asiento = "B2", FechaReserva = DateTime.Today.AddDays(1), EstadoReserva = "RETRASO", Precio = 8f },
            new Reserva { ReservaId = 3, Asiento = "C3", FechaReserva = DateTime.Today.AddDays(2), EstadoReserva = "A TIEMPO", Precio = 8f }
        };

        MostrarQRCommand = new Command<Reserva>(async (reserva) => await MostrarQRAmpliado(reserva));
    }

    private async Task MostrarQRAmpliado(Reserva reserva)
    {
        await Application.Current.MainPage.Navigation.PushModalAsync(new QRCodePopup("qr_code.jpeg"));
    }

    // Método para obtener el color según el estado
    public Color GetColorForEstado(string estadoReserva)
    {
        return estadoReserva == "A TIEMPO" ? Colors.Green : Colors.Red;
    }
    
}