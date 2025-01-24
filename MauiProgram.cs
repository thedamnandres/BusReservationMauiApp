using BusReservationMauiApp;
using BusReservationMauiApp.Services;
using BusReservationMauiApp.ViewModels;
using BusReservationMauiApp.Views;
using BusReservationMauiApp.Views.Reservas;
using Microsoft.Extensions.Logging;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Registro de servicios
        builder.Services.AddSingleton<RutaService>();
        builder.Services.AddSingleton<RutaViewModel>();

        // Registro de vistas
        builder.Services.AddSingleton<RutaPage>();
        
        builder.Services.AddSingleton<BoletoPage>();
        builder.Services.AddSingleton<BoletoViewModel>();

        builder.Services.AddSingleton<ReservaViewModel>();
        builder.Services.AddSingleton<ReservaPage>();
        
        
#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}