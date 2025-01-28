using Microsoft.Maui.Controls;

namespace BusReservationMauiApp.Views.Usuario
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // Asegúrate de que la compilación XAML esté habilitada
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // Lógica de autenticación aquí
            var appShell = (AppShell)Application.Current.MainPage;
            appShell.ShowMainTabBar();
            // Si la autenticación es exitosa, muestra las pestañas principales
            ((AppShell)Shell.Current).ShowMainTabBar();
            await Shell.Current.GoToAsync("//Principal");
        }
    }
}