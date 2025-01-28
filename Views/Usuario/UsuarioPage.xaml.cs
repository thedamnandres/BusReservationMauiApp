using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.ViewModels;

namespace BusReservationMauiApp.Views;

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
