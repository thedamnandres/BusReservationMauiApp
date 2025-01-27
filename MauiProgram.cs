using BusReservationMauiApp;
using BusReservationMauiApp.Services;
using BusReservationMauiApp.ViewModels;
using BusReservationMauiApp.Views;
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

        // Rutas
        builder.Services.AddSingleton<RutaService>();
        builder.Services.AddSingleton<RutaViewModel>();
        builder.Services.AddSingleton<RutaPage>();
        
        // Boletos
        builder.Services.AddSingleton<BoletoService>();
        builder.Services.AddSingleton<BoletoViewModel>();
        builder.Services.AddSingleton<BoletoPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}