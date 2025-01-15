using BusReservationMauiApp.ViewModels;

namespace BusReservationMauiApp.Views;

public partial class RutaPage : ContentPage
{
   //rut page
      public RutaPage(RutaViewModel viewModel)
      {
         InitializeComponent();
         BindingContext = viewModel;
      }
}