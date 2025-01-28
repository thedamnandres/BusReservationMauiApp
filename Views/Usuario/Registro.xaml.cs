using BusReservationMauiApp.ViewModels;

namespace BusReservationMauiApp.Views.Usuario
{
    public partial class Registro : ContentPage
    {
        public Registro()
        {
            InitializeComponent();
            BindingContext = new RegistroViewModel(); // Establecer el contexto de la vista
        }

        // L�gica para el bot�n "Volver"
        private async void btnVolver_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync(); // Navega hacia atr�s
        }
    }
}
