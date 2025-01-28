using BusReservationMauiApp.Interfaces;

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

   
    static SQLDatos _bancoDatos;

    public static SQLDatos BancoDatos
    {
        get
        {
            if (_bancoDatos == null)
            {
                _bancoDatos =
                    new SQLDatos(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Datos.db"));

            }
            return _bancoDatos;



        }




    }

}

