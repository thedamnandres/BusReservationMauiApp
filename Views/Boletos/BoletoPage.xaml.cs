using System;
using System.Collections.ObjectModel;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.ViewModels;
using Microsoft.Maui.Controls;

namespace BusReservationMauiApp.Views;

public partial class BoletoPage : ContentPage
{
    public ObservableCollection<Reserva> Reservas { get; set; }
    
    public BoletoPage()
    {
        InitializeComponent();
        BindingContext = new BoletoViewModel();
    }
    
}
