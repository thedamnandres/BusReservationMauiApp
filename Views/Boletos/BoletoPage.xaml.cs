using System;
using System.Collections.ObjectModel;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.Views.Boletos;
using Microsoft.Maui.Controls;

namespace BusReservationMauiApp.Views;

public partial class BoletoPage : ContentPage
{
    public ObservableCollection<Reserva> Reservas { get; set; }

    public BoletoPage()
    {
        InitializeComponent();
        
        Reservas = new ObservableCollection<Reserva>
        {
            new Reserva { ReservaId = 1, Asiento = "A1", FechaReserva = DateTime.Today, EstadoReserva = "A TIEMPO", Precio = 8f },
            new Reserva { ReservaId = 2, Asiento = "B2", FechaReserva = DateTime.Today.AddDays(1), EstadoReserva = "RETRASO", Precio = 8f },
            new Reserva { ReservaId = 3, Asiento = "C3", FechaReserva = DateTime.Today.AddDays(2), EstadoReserva = "A TIEMPO", Precio = 8f }
        };
        
        CargarBoletos();
    }

    private void CargarBoletos()
    {
        BoletosGrid.RowDefinitions.Clear();
        BoletosGrid.Children.Clear();

        int rowIndex = 0;

        foreach (var reserva in Reservas)
        {
            BoletosGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Frame para la reserva
            var frame = new Frame
            {
                CornerRadius = 10,
                Padding = 10,
                Margin = new Thickness(10, 5, 10, 5),
                BackgroundColor = Colors.White,
                HasShadow = true
            };

            var grid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = GridLength.Star },
                    new ColumnDefinition { Width = GridLength.Auto } 
                },
                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };

            // Atributos
            var infoStack = new VerticalStackLayout
            {
                Spacing = 5,
                Children =
                {
                    new Label
                    {
                        Text = $"Asiento: {reserva.Asiento}",
                        FontSize = 18,
                        FontAttributes = FontAttributes.Bold,
                        TextColor = Colors.Black
                    },
                    new Label
                    {
                        Text = $"Estado: {reserva.EstadoReserva}",
                        FontSize = 14,
                        TextColor = reserva.EstadoReserva == "A TIEMPO" ? Colors.Green : Colors.Red
                    },
                    new Label
                    {
                        Text = $"Fecha: {reserva.FechaReserva:dd/MM/yyyy}",
                        FontSize = 14,
                        TextColor = Colors.Gray
                    }
                }
            };

            grid.Add(infoStack, 0, 0);
            Grid.SetRowSpan(infoStack, 2);

            // QR Code
            var qrImage = new Image
            {
                Source = "qr_code.jpeg", 
                HeightRequest = 100,
                WidthRequest = 100,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Center
            };

            // Evento táctil para ampliar el QR
            var tapGesture = new TapGestureRecognizer();
            tapGesture.Tapped += async (s, e) =>
            {
                await MostrarQRAmpliado(reserva);
            };
            qrImage.GestureRecognizers.Add(tapGesture);

            grid.Add(qrImage, 1, 0);
            Grid.SetRowSpan(qrImage, 2);
            
            frame.Content = grid;

            // Addframe al grid principal
            BoletosGrid.Add(frame, 0, rowIndex);
            rowIndex++;
        }
    }
    
    private async Task MostrarQRAmpliado(Reserva reserva)
    {
        await Navigation.PushModalAsync(new QRCodePopup("qr_code.jpeg"));
    }
}
