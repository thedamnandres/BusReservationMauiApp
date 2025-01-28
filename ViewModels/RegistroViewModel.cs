using BusReservationMauiApp.Models;
using MvvmHelpers;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace BusReservationMauiApp.ViewModels
{
    public class RegistroViewModel : BaseViewModel
    {
        private Usuario _user;
        private string _nombre;
        private string _contrasenia;
        private string _ci;
        private string _correo;
        private string _telefono;

        public string Nombre
        {
            get => _nombre;
            set => SetProperty(ref _nombre, value);
        }

        public string Contrasenia
        {
            get => _contrasenia;
            set => SetProperty(ref _contrasenia, value);
        }

        public string CI
        {
            get => _ci;
            set => SetProperty(ref _ci, value);
        }

        public string Correo
        {
            get => _correo;
            set => SetProperty(ref _correo, value);
        }

        public string Telefono
        {
            get => _telefono;
            set => SetProperty(ref _telefono, value);
        }

        // Comando para el registro
        public ICommand RegisterCommand { get; }

        public RegistroViewModel()
        {
            // Inicializamos el comando con su método correspondiente
            RegisterCommand = new Command(async () => await Register());
        }

        // Lógica de registro
        private async Task Register()
        {
            // Verificar que los campos no estén vacíos
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Contrasenia) ||
                string.IsNullOrEmpty(CI) || string.IsNullOrEmpty(Correo) || string.IsNullOrEmpty(Telefono))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Por favor complete todos los campos", "OK");
                return;
            }

            // Crear el usuario con los valores proporcionados
            _user = new Usuario
            {
                Nombre = Nombre,
                contrasenia = Contrasenia, // Corrige la capitalización si es necesario
                CI = CI,
                Correo = Correo,
                Telefono = Telefono
            };

            // Intentar guardar el usuario en la base de datos
            var registro = await App.BancoDatos.UserDataTabla.GuardarUsuario(_user);

            if (registro > 0)
            {
                await Application.Current.MainPage.DisplayAlert("Éxito", "Usuario registrado correctamente", "OK");
                // Después de registrar, podemos navegar de regreso
                await Application.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "No se pudo registrar el usuario", "OK");
            }
        }
    }
}
