using BusReservationMauiApp.ViewModels;

namespace BusReservationMauiApp.Views.Reservas;

public partial class ReservaPage : ContentPage
{
    public ReservaPage()
    {
        InitializeComponent();
        BindingContext = new ReservaViewModel();
    }
}