using BusReservationMauiApp.Models;
using MvvmHelpers;

namespace BusReservationMauiApp.ViewModels
{
    public class UsuarioViewModel : BaseViewModel
    {
        private Usuario _usuario;

        // Propiedad UsuarioActual para el Binding en el XAML
        public Usuario UsuarioActual
        {
            get => _usuario;
            set
            {
                SetProperty(ref _usuario, value);
                // Notificar cambios de propiedades individuales
                OnPropertyChanged(nameof(Nombre));
                OnPropertyChanged(nameof(CI));
                OnPropertyChanged(nameof(Correo));
                OnPropertyChanged(nameof(Telefono));
            }
        }

        // Propiedades para el Binding en XAML
        public string Nombre => UsuarioActual?.Nombre ?? string.Empty;
        public string CI => UsuarioActual?.CI ?? string.Empty;
        public string Correo => UsuarioActual?.Correo ?? string.Empty;
        public string Telefono => UsuarioActual?.Telefono ?? string.Empty;

        // Constructor
        public UsuarioViewModel()
        {
            // Cargar datos del usuario desde la base de datos, API, o cualquier fuente real
            CargarUsuario();
        }

        // Método para cargar el usuario desde una fuente de datos
        private async void CargarUsuario()
        {
            try
            {
                // Simulación de carga desde una base de datos o servicio
                UsuarioActual = await App.BancoDatos.UserDataTabla.ObtenerUltimoUsuario();

                if (UsuarioActual == null)
                {
                    // Si no hay usuario en la base, inicializamos un usuario vacío
                    UsuarioActual = new Usuario();
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores si ocurre un problema al cargar los datos
                await Application.Current.MainPage.DisplayAlert("Error", $"No se pudo cargar el usuario: {ex.Message}", "OK");
                UsuarioActual = new Usuario(); // Usuario vacío para evitar nulls
            }
        }
    }
}
