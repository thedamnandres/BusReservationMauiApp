using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusReservationMauiApp.Models;

namespace BusReservationMauiApp.Views.Reservas;

public partial class ReservaPage : ContentPage
{
    public ObservableCollection<Reserva> Reservas { get; set; }

    public ReservaPage()
    {
        InitializeComponent();

        // Reservas quemadas iniciales
        Reservas = new ObservableCollection<Reserva>
        {
            new Reserva { ReservaId = 1, Asiento = "A1", FechaReserva = DateTime.Today, EstadoReserva = "APROBADO", Precio = 10.50f },
            new Reserva { ReservaId = 2, Asiento = "B2", FechaReserva = DateTime.Today.AddDays(1), EstadoReserva = "APROBADO", Precio = 12.00f },
            new Reserva { ReservaId = 3, Asiento = "C3", FechaReserva = DateTime.Today.AddDays(2), EstadoReserva = "APROBADO", Precio = 8.75f }
        };

        // Generar la vista para cada reserva
        CargarReservas();
    }

    private void CargarReservas()
    {
        ReservasGrid.RowDefinitions.Clear();
        ReservasGrid.Children.Clear();

        int rowIndex = 0;

        foreach (var reserva in Reservas)
        {
            // Agregar una nueva fila al grid
            ReservasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Crear un frame para la reserva
            var frame = new Frame
            {
                CornerRadius = 10,
                Padding = 15,
                Margin = new Thickness(10, 5, 10, 5),
                BackgroundColor = Colors.White,
                HasShadow = true,
                Content = new Grid
                {
                    ColumnDefinitions =
                    {
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Star },
                        new ColumnDefinition { Width = GridLength.Star }
                    },
                    RowDefinitions =
                    {
                        new RowDefinition { Height = GridLength.Auto },
                        new RowDefinition { Height = GridLength.Auto }
                    }
                }
            };

            // Crear elementos visuales dentro del frame
            var asientoLabel = new Label
            {
                Text = $"Asiento: {reserva.Asiento}",
                FontSize = 18,
                FontAttributes = FontAttributes.Bold,
                TextColor = Colors.Black,
                HorizontalOptions = LayoutOptions.Start
            };
            Grid.SetRow(asientoLabel, 0);
            Grid.SetColumn(asientoLabel, 0);

            var fechaLabel = new Label
            {
                Text = $"Fecha: {reserva.FechaReserva:dd/MM/yyyy}",
                FontSize = 14,
                TextColor = Colors.Gray,
                HorizontalOptions = LayoutOptions.Center
            };
            Grid.SetRow(fechaLabel, 0);
            Grid.SetColumn(fechaLabel, 1);

            var estadoLabel = new Label
            {
                Text = $"Estado: {reserva.EstadoReserva}",
                FontSize = 14,
                TextColor = reserva.EstadoReserva == "APROBADO" ? Colors.Green : Colors.Red,
                HorizontalOptions = LayoutOptions.End
            };
            Grid.SetRow(estadoLabel, 0);
            Grid.SetColumn(estadoLabel, 2);

            var precioLabel = new Label
            {
                Text = $"Precio: ${reserva.Precio:F2}",
                FontSize = 14,
                TextColor = Colors.Gray,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Start
            };
            Grid.SetRow(precioLabel, 1);
            Grid.SetColumnSpan(precioLabel, 3);

            // Agregar elementos al grid dentro del frame
            var frameGrid = (Grid)frame.Content;
            frameGrid.Children.Add(asientoLabel);
            frameGrid.Children.Add(fechaLabel);
            frameGrid.Children.Add(estadoLabel);
            frameGrid.Children.Add(precioLabel);

            // Agregar frame al grid principal
            ReservasGrid.Add(frame, 0, rowIndex);
            rowIndex++;
        }
    }

    private async void OnAddReservaClicked(object sender, EventArgs e)
    {
        await DisplayAlert("No permitido", "No se pueden agregar reservas en esta página.", "OK");
    }
}