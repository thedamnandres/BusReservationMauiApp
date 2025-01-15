namespace BusReservationMauiApp;

public partial class App : Application
{
    public static HttpClient HttpClient { get; private set; }
    public App()
    {
        InitializeComponent();
        
        HttpClient = new HttpClient
        {
            BaseAddress = new Uri("https://localhost:5001/api/")
        };

        MainPage = new AppShell();
    }
}

