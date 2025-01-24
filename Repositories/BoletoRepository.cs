using BusReservationMauiApp.Interfaces;
using BusReservationMauiApp.Models;
using SQLite;

namespace BusReservationMauiApp.Repositories;

public class BoletoRepository : iBoletosRepository
{
    private readonly SQLiteConnection _connection;

    public BoletoRepository(string dbPath)
    {
        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTable<Boleto>();
    }

    public async Task<IEnumerable<Boleto>> GetBoletosAsync()
    {
        // Obtener todos los boletos
        return await Task.Run(() => _connection.Table<Boleto>().ToList());  
    }

    public async Task<bool> CreateBoletoAsync(Boleto boleto)
    {
        try
        {
            _connection.Insert(boleto);
            return await Task.FromResult(true);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    public async Task<bool> UpdateBoletoAsync(Boleto boleto)
    {
        try
        {
            _connection.Update(boleto);
            return await Task.FromResult(true);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    public async Task<bool> DeleteBoletoAsync(int boletoId)
    {
        try
        {
            _connection.Delete<Boleto>(boletoId);
            return await Task.FromResult(true);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    public async Task<IEnumerable<Boleto>> GetBoletosByReservaAsync(int reservaId)
    {
        var boletos = _connection.Table<Boleto>().Where(b => b.ReservaId == reservaId).ToList();
        return await Task.FromResult(boletos);
    }

    public async Task<Boleto> GetBoletoAsync(int boletoId)
    {
        return await Task.FromResult(_connection.Find<Boleto>(boletoId));
    }

}