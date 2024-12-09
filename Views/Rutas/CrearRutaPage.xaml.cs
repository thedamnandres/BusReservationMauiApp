using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.Repositoies;

namespace BusReservationMauiApp.Views;

public partial class CrearRutaPage : ContentPage
{
     private readonly RutaRepository _repository;
    private readonly Ruta _ruta; // Ruta para edición (opcional)
    private readonly bool _isEditing;

    public CrearRutaPage(RutaRepository repository, Ruta ruta = null)
    {
        InitializeComponent();
        _repository = repository;
        _ruta = ruta;
        _isEditing = ruta != null;

        // Ajustar el título dinámicamente
        Title = _isEditing ? "Editar Ruta" : "Nueva Ruta";

        if (_isEditing)
        {
            PopulateFields();
        }
    }

    private void PopulateFields()
    {
        // Inicializar los campos con los valores de la ruta
        OrigenEntry.Text = _ruta.Origen;
        DestinoEntry.Text = _ruta.Destino;
        DuracionPicker.Time = _ruta.Duracion;
        HoraPicker.Time = _ruta.Hora;
        FechaPicker.Date = _ruta.Fecha;
    }

    private async void OnSaveRutaClicked(object sender, EventArgs e)
    {
        // Validación básica
        if (string.IsNullOrEmpty(OrigenEntry.Text) || string.IsNullOrEmpty(DestinoEntry.Text))
        {
            await DisplayAlert("Error", "Por favor completa todos los campos.", "OK");
            return;
        }

        if (_isEditing)
        {
            // Actualizar ruta existente
            _ruta.Origen = OrigenEntry.Text;
            _ruta.Destino = DestinoEntry.Text;
            _ruta.Duracion = DuracionPicker.Time;
            _ruta.Hora = HoraPicker.Time;
            _ruta.Fecha = FechaPicker.Date;

            await _repository.UpdateRutaAsync(_ruta);
            await DisplayAlert("Éxito", "Ruta actualizada con éxito.", "OK");
        }
        else
        {
            // Crear nueva ruta
            var nuevaRuta = new Ruta
            {
                IdRuta = new Random().Next(100, 1000), // Generar ID temporal
                Origen = OrigenEntry.Text,
                Destino = DestinoEntry.Text,
                Duracion = DuracionPicker.Time,
                Hora = HoraPicker.Time,
                Fecha = FechaPicker.Date
            };

            await _repository.AddRutaAsync(nuevaRuta);
            await DisplayAlert("Éxito", "Ruta creada con éxito.", "OK");
        }

        MessagingCenter.Send(this, "RutaActualizada");

        // Regresar a la página anterior
        await Navigation.PopAsync();
    }
    
    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }
}