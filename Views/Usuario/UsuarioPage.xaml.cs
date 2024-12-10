using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusReservationMauiApp.Models;

namespace BusReservationMauiApp.Views;

public partial class UsuarioPage : ContentPage
{
    public UsuarioPage()
    {
        InitializeComponent();

        // Datos quemados
        var usuario = new Usuario
        {
            Nombre = "Paul Larrea",
            CI = "1765467382",
            Correo = "paul.larrea@hotmail.com",
            Telefono = "99 897 7653"
        };

        // Asignar el modelo como contexto de la vista
        BindingContext = usuario;
    }
}