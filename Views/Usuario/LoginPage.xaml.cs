using Microsoft.Maui.Controls;

namespace BusReservationMauiApp.Views.Usuario
{
    [XamlCompilation(XamlCompilationOptions.Compile)] // Aseg�rate de que la compilaci�n XAML est� habilitada
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            // L�gica de autenticaci�n aqu�
            var appShell = (AppShell)Application.Current.MainPage;
            appShell.ShowMainTabBar();
            // Si la autenticaci�n es exitosa, muestra las pesta�as principales
            ((AppShell)Shell.Current).ShowMainTabBar();
            await Shell.Current.GoToAsync("//Principal");
        }
    }
}