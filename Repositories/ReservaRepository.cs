using BusReservationMauiApp.Interfaces;
using BusReservationMauiApp.Models;
using SQLite;

namespace BusReservationMauiApp.Repositories;

public class ReservaRepository : iReservaRespository
{
    private readonly SQLiteConnection _connection;

    public ReservaRepository(string dbPath)
    {
        _connection = new SQLiteConnection(dbPath);
        _connection.CreateTable<Reserva>();
    }

    public async Task<IEnumerable<Reserva>> GetReservasAsync()
    {
        // Obtener todas las reservas
        return await Task.Run(() => _connection.Table<Reserva>().ToList());  
    }
    public async Task<bool> CreateReservaAsync(Reserva reserva)
    {
        try
        {
            _connection.Insert(reserva);
            return await Task.FromResult(true);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    public async Task<bool> UpdateReservaAsync(Reserva reserva)
    {
        try
        {
            _connection.Update(reserva);
            return await Task.FromResult(true);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    public async Task<bool> DeleteReservaAsync(int reservaId)
    {
        try
        {
            _connection.Delete<Reserva>(reservaId);
            return await Task.FromResult(true);
        }
        catch
        {
            return await Task.FromResult(false);
        }
    }

    public async Task<IEnumerable<Reserva>> GetReservasByClienteAsync(int clienteId)
    {
        var reservas = _connection.Table<Reserva>().Where(r => r.ClienteId == clienteId).ToList();
        return await Task.FromResult(reservas);
    }

    public async Task<Reserva> GetReservaAsync(int reservaId)
    {
        return await Task.FromResult(_connection.Find<Reserva>(reservaId));
    }

}