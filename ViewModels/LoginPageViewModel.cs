
using BusReservationMauiApp.Views.Usuario;
using MvvmHelpers;
using System.Windows.Input;

namespace BusReservationMauiApp.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        private string _username;
        public string Username
        {
            get => _username;
            set => SetProperty(ref _username, value); // Actualiza el valor y notifica cambios a la vista.
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        // Comandos para los botones
        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }
        public ICommand ForgotPasswordCommand { get; }

        public LoginViewModel()
        {
            // Inicializamos los comandos con sus métodos respectivos
            LoginCommand = new Command(async () => await Login());
            RegisterCommand = new Command(async () => await Register());

            LoadUsers();
        }

        // Lógica del inicio de sesión
        private async Task Login()
        {
            if (string.IsNullOrWhiteSpace(Username) || string.IsNullOrWhiteSpace(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor ingrese usuario y contraseña", "OK");
                return;
            }

            var user = await App.BancoDatos.UserDataTabla.ObtenerUsuario(Username, Password);

            if (user == null)
            {
                await Application.Current.MainPage.DisplayAlert("Inicio de Sesión Fallido", "Usuario o Contraseña Incorrectos", "OK");
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Inicio de Sesión", "Inicio de Sesión Exitoso", "OK");

                // Navegar a la Shell principal después del login exitoso
                Application.Current.MainPage = new AppShell();  // Cambia la página raíz a AppShell
                await Shell.Current.GoToAsync("///Principal");
            }
        }


        // Lógica para registrarse
        private async Task Register()
        {
            await Application.Current.MainPage.Navigation.PushAsync(new Registro());
        }



        private void LoadUsers()
        {
            var lista = App.BancoDatos.UserDataTabla.ListaUsuarios().Result;
            // Aquí puedes hacer algo con la lista si es necesario.
        }
    }
}
