using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusReservationMauiApp.Repositoies;
using BusReservationMauiApp.Models;

namespace BusReservationMauiApp.Views;

public partial class RutaPage : ContentPage
{
    private readonly RutaRepository _repository;

    public RutaPage()
    {
        InitializeComponent();
        _repository = new RutaRepository();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadRutas();
    }

    private async void LoadRutas()
{
    var rutas = await _repository.GetAllRutasAsync();

    // Limpiar las filas previas del grid
    RutasGrid.RowDefinitions.Clear();
    RutasGrid.Children.Clear();

    int rowIndex = 0;

    foreach (var ruta in rutas)
    {
        // Crear una fila para cada ruta
        RutasGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

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

        // Crear los elementos visuales
        var horaSalidaLabel = new Label
        {
            Text = $"{ruta.Origen}",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.Start,
            VerticalOptions = LayoutOptions.Center
        };
        Grid.SetRow(horaSalidaLabel, 0);
        Grid.SetColumn(horaSalidaLabel, 0);

        var duracionLayout = new VerticalStackLayout
        {
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Center,
            Spacing = 3,
            Children =
            {
                new Label
                {
                    Text = $"Salida: {ruta.Hora.Add(ruta.Hora).ToString(@"hh\:mm")}",
                    FontSize = 14,
                    TextColor = Colors.Gray,
                    HorizontalOptions = LayoutOptions.Center
                },
                new Image
                {
                    Source = "bus.png",
                    HeightRequest = 40,
                    WidthRequest = 40,
                    HorizontalOptions = LayoutOptions.Center
                }
            }
        };
        Grid.SetRow(duracionLayout, 0);
        Grid.SetColumn(duracionLayout, 1);

        var horaLlegadaLabel = new Label
        {
            Text = $"{ruta.Destino}",
            FontSize = 18,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black,
            HorizontalOptions = LayoutOptions.End,
            VerticalOptions = LayoutOptions.Center
        };
        Grid.SetRow(horaLlegadaLabel, 0);
        Grid.SetColumn(horaLlegadaLabel, 2);

        var trayectoLabel = new Label
        {
            Text = $"Duración {ruta.Duracion.Hours}h {ruta.Duracion.Minutes}m",
            FontSize = 14,
            TextColor = Colors.Gray,
            HorizontalOptions = LayoutOptions.Center,
            VerticalOptions = LayoutOptions.Start
        };
        Grid.SetRow(trayectoLabel, 1);
        Grid.SetColumnSpan(trayectoLabel, 3);

        // Agregar evento táctil al frame
        var tapGestureRecognizer = new TapGestureRecognizer();
        tapGestureRecognizer.Tapped += async (s, e) =>
        {
            var action = await DisplayActionSheet(
                $"Ruta: {ruta.Origen} → {ruta.Destino}",
                "Cancelar",
                null,
                "Editar",
                "Eliminar");

            if (action == "Editar")
            {
                await OnEditRutaClicked(ruta);
            }
            else if (action == "Eliminar")
            {
                await OnDeleteRutaClicked(ruta);
            }
        };
        frame.GestureRecognizers.Add(tapGestureRecognizer);

        // Agregar los elementos al grid del frame
        var frameGrid = (Grid)frame.Content;
        frameGrid.Children.Add(horaSalidaLabel);
        frameGrid.Children.Add(duracionLayout);
        frameGrid.Children.Add(horaLlegadaLabel);
        frameGrid.Children.Add(trayectoLabel);

        // Agregar el frame al grid principal
        RutasGrid.Add(frame, 0, rowIndex);
        rowIndex++;
    }
}

private async Task OnEditRutaClicked(Ruta ruta)
{
    await Navigation.PushAsync(new CrearRutaPage(_repository, ruta));
}

private async Task OnDeleteRutaClicked(Ruta ruta)
{
    var confirm = await DisplayAlert(
        "Confirmar",
        $"¿Estás seguro de eliminar la ruta de {ruta.Origen} a {ruta.Destino}?",
        "Sí",
        "No");

    if (confirm)
    {
        await _repository.DeleteRutaAsync(ruta.IdRuta);
        LoadRutas();
    }
}


    private async void OnAddRutaClicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new CrearRutaPage(_repository));
    }
    
}
