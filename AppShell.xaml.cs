using Microsoft.Maui.Controls;
namespace BusReservationMauiApp;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute("LoginPage", typeof(Views.Usuario.LoginPage));
        Routing.RegisterRoute("Principal", typeof(Views.HomePage));
        Routing.RegisterRoute("Rutas", typeof(Views.RutaPage));
        Routing.RegisterRoute("Boletos", typeof(Views.BoletoPage));
        Routing.RegisterRoute("Reservas", typeof(Views.Reservas.ReservaPage));
        Routing.RegisterRoute("Usuario", typeof(Views.UsuarioPage));
    }



    public void ShowMainTabBar()
    {
        MainTabBar.IsVisible = true;
    }
}