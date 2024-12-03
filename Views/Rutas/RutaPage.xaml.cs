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
        RutasCollectionView.ItemsSource = null; // Limpiar datos antiguos
        RutasCollectionView.ItemsSource = rutas; // Cargar nuevas rutas
    }

    // Evento para agregar una nueva ruta
    private async void OnAddRutaClicked(object sender, EventArgs e)
    {
        // Navegar a CreateRutaPage para crear una nueva ruta
        await Navigation.PushAsync(new CrearRutaPage(_repository));
    }

    // Evento para editar una ruta
    private async void OnEditRutaClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is Ruta ruta)
        {
            // Navegar a CreateRutaPage para editar la ruta seleccionada
            await Navigation.PushAsync(new CrearRutaPage(_repository, ruta));
        }
    }

    // Evento para eliminar una ruta
    private async void OnDeleteRutaClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        if (button?.BindingContext is Ruta ruta)
        {
            // Confirmar antes de eliminar
            var confirm = await DisplayAlert("Confirmar", $"¿Estás seguro de eliminar la ruta de {ruta.Origen} a {ruta.Destino}?", "Sí", "No");
            if (confirm)
            {
                await _repository.DeleteRutaAsync(ruta.IdRuta);
                LoadRutas(); // Recargar rutas después de eliminar
            }
        }
    }
}