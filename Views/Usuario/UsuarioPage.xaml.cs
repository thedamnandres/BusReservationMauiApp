using System;
using System.Threading.Tasks;
using BusReservationMauiApp.ViewModels;
using Microsoft.Maui.Controls;

namespace BusReservationMauiApp.Views
{
    public partial class UsuarioPage : ContentPage
    {
        public UsuarioPage()
        {
            InitializeComponent();
            BindingContext = new UsuarioViewModel();
        }

        private async void btnCerrarSesion_Clicked(object sender, EventArgs e)
        {
            // Limpia datos de sesión
            await Shell.Current.GoToAsync("//LoginPage");
        }
    }
}