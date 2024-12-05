using System;
using System.Collections.ObjectModel;
using BusReservationMauiApp.Models;
using Microsoft.Maui.Controls;

namespace BusReservationMauiApp.Views
{
    public partial class ReservaPage : ContentPage
    {
        // Usamos ObservableCollection para que la interfaz se actualice automáticamente
        public ObservableCollection<Reserva> Reservas { get; set; }
        private int reservaId = 4; // Para incrementar el ID automáticamente
        private char letraAsiento = 'D'; // Empezamos desde la letra D para los asientos

        public ReservaPage()
        {
            InitializeComponent();

            // Datos iniciales
            Reservas = new ObservableCollection<Reserva>
            {
                new Reserva { ReservaId = 1, Asiento = "A1", FechaReserva = DateTime.Today, EstadoReserva = "APROBADO", Precio = 10.50f },
                new Reserva { ReservaId = 2, Asiento = "B2", FechaReserva = DateTime.Today.AddDays(1), EstadoReserva = "APROBADO", Precio = 12.00f },
                new Reserva { ReservaId = 3, Asiento = "C3", FechaReserva = DateTime.Today.AddDays(2), EstadoReserva = "APROBADO", Precio = 8.75f }
            };

            // Establecer el contexto de enlace a la vista
            BindingContext = this;
        }

        private async void OnAddReservaClicked(object sender, EventArgs e)
        {
            // Crear nueva reserva con datos que se suman secuencialmente
            var nuevaReserva = new Reserva
            {
                ReservaId = reservaId++, // Incrementa el ID automáticamente
                Asiento = $"{letraAsiento++}{new Random().Next(1, 10)}", // Asientos con letra y número
                FechaReserva = DateTime.Today.AddDays(3),
                EstadoReserva = "APROBADO", // El estado siempre será "APROBADO"
                Precio = (float)new Random().NextDouble() * (15 - 8) + 8 // Precio aleatorio entre 8 y 15
            };

            // Agregar la nueva reserva a la colección observable
            Reservas.Add(nuevaReserva);

            // Mostrar un mensaje de éxito
            await DisplayAlert("Aprobado", $"Reserva {nuevaReserva.ReservaId} agregada exitosamente.", "OK");
        }
    }
}
