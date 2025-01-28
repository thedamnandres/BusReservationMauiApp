using System;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.Json;
using BusReservationMauiApp.Models;
using BusReservationMauiApp.ViewModels;
using BusReservationMauiApp.Views.Boletos;
using Microsoft.Maui.Controls;

namespace BusReservationMauiApp.Views;

public partial class BoletoPage : ContentPage
{
    public ObservableCollection<Boleto> Boletos { get; set; } = new();

    public BoletoPage()
    {
        InitializeComponent();
        BindingContext = this;

        // Cargar todos los boletos al inicializar la página
        CargarTodosLosBoletos();
    }

    // Método para cargar todos los boletos desde la API
    private async void CargarTodosLosBoletos()
    {
        try
        {
            var client = new HttpClient();
            var response = await client.GetStringAsync("http://localhost:5191/api/Boletos/ver-boletos");
            var boletos = JsonSerializer.Deserialize<List<Boleto>>(response, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            Boletos.Clear();
            if (boletos != null)
            {
                foreach (var boleto in boletos)
                {
                    Boletos.Add(boleto);
                }
            }
        }
        catch (HttpRequestException ex)
        {
            await DisplayAlert("Error", $"Error de conexión: {ex.Message}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }


    // Método para buscar un boleto por reservaId
    private async void BuscarBoletoPorReservaId()
    {
        try
        {
            // Solicitar el reservaId al usuario
            string reservaId = await DisplayPromptAsync("Buscar Boleto", "Ingrese el ID de la reserva:");

            // Si el usuario presiona "Cancelar", reservaId será null, así que salimos del método
            if (reservaId == null)
            {
                return;  // Si el usuario cancela, no hacemos nada más
            }

            if (string.IsNullOrWhiteSpace(reservaId))
            {
                await DisplayAlert("Error", "Debe ingresar un ID de reserva válido.", "OK");
                return;
            }

            // Realizar la solicitud GET al API
            var client = new HttpClient();
            var response = await client.GetAsync($"http://localhost:5191/api/Boletos/ver-boleto/{reservaId}");

            if (response.IsSuccessStatusCode)
            {
                // Leer y deserializar el resultado
                var json = await response.Content.ReadAsStringAsync();
                var boleto = JsonSerializer.Deserialize<Boleto>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Validar que el boleto no sea nulo
                if (boleto != null)
                {
                    await DisplayAlert(
                        "Boleto Encontrado",
                        $"Reserva ID: {boleto.ReservaId}\n" +
                        $"Nombre Pasajero: {boleto.NombrePasajero ?? "Sin Nombre"}\n" +
                        $"Asiento: {boleto.Asiento ?? "Sin Asignar"}\n" +
                        $"Fecha de Viaje: {(boleto.FechaViaje != default ? boleto.FechaViaje.ToString("yyyy-MM-dd") : "No Disponible")}\n" +
                        $"Precio: {(boleto.Precio != 0 ? boleto.Precio.ToString("C") : "No Disponible")}\n" +
                        $"Estado de Reserva: {boleto.EstadoReserva ?? "Desconocido"}",
                        "OK");
                }
                else
                {
                    await DisplayAlert("Error", "No se encontró un boleto con el ID proporcionado.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "No se encontró un boleto con el ID proporcionado.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al buscar el boleto: {ex.Message}", "OK");
        }
    }



    // Vincular el evento del botón en el código detrás
    private void OnBuscarBoletoClicked(object sender, EventArgs e)
    {
        BuscarBoletoPorReservaId();
    }


    private async void EliminarBoletoPorReservaId()
    {
        try
        {
            // Solicitar el reservaId al usuario
            string reservaId = await DisplayPromptAsync("Eliminar Boleto", "Ingrese el ID de la reserva a eliminar:");

            // Si el usuario presiona "Cancelar", reservaId será null, así que salimos del método
            if (reservaId == null)
            {
                return;  // Si el usuario cancela, no hacemos nada más
            }

            if (string.IsNullOrWhiteSpace(reservaId))
            {
                await DisplayAlert("Error", "Debe ingresar un ID de reserva válido.", "OK");
                return;
            }

            // Obtener información del boleto antes de eliminar
            var client = new HttpClient();
            var responseInfo = await client.GetAsync($"http://localhost:5191/api/Boletos/ver-boleto/{reservaId}");

            if (responseInfo.IsSuccessStatusCode)
            {
                var jsonResponse = await responseInfo.Content.ReadAsStringAsync();
                var boleto = JsonSerializer.Deserialize<Boleto>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                if (boleto != null)
                {
                    // Mostrar la información del boleto y pedir confirmación
                    string mensaje = $"Información del boleto:\n\n" +
                                     $"ID Reserva: {boleto.ReservaId}\n" +
                                     $"Nombre Pasajero: {boleto.NombrePasajero}\n" +
                                     $"Asiento: {boleto.Asiento}\n" +
                                     $"Fecha Viaje: {boleto.FechaViaje.ToShortDateString()}\n" +
                                     $"Precio: {boleto.Precio:C}\n" +
                                     $"Estado Reserva: {boleto.EstadoReserva}\n\n" +
                                     $"¿Está seguro de que desea eliminar este boleto?";

                    // Si el usuario presiona "Cancelar" en la confirmación, no se hace nada
                    bool confirmacion = await DisplayAlert("Confirmación", mensaje, "Sí", "No");
                    if (!confirmacion)
                    {
                        return; // Si el usuario cancela la confirmación, no hacer nada
                    }

                    // Realizar la solicitud DELETE al API
                    var responseDelete = await client.DeleteAsync($"http://localhost:5191/api/Boletos/eliminar/{reservaId}");

                    if (responseDelete.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Éxito", "El boleto se eliminó correctamente.", "OK");

                        // Actualizar la lista de boletos
                        CargarTodosLosBoletos();
                    }
                    else
                    {
                        await DisplayAlert("Error",
                            "No se pudo eliminar el boleto. Asegúrese de que el ID de reserva exista.", "OK");
                    }
                }
                else
                {
                    await DisplayAlert("Error", "No se encontró el boleto con el ID proporcionado.", "OK");
                }
            }
            else
            {
                await DisplayAlert("Error", "No se encontró el boleto con el ID proporcionado.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error al eliminar el boleto: {ex.Message}", "OK");
        }
    }


    private async void OnBuscarClicked(object sender, EventArgs e)
    {
        // Método que busca un boleto por reservaId
        BuscarBoletoPorReservaId();
    }

    private void OnEliminarPorReservaIdClicked(object sender, EventArgs e)
    {
        EliminarBoletoPorReservaId();
    }


    private async void EditarBoleto(Boleto boleto)
    {
        try
        {
            // Solicitar nuevos datos al usuario
            string nuevoNombrePasajero = await DisplayPromptAsync("Editar Boleto",
                "Ingrese el nuevo nombre del pasajero:", initialValue: boleto.NombrePasajero);
            if (nuevoNombrePasajero == null) return;  // Si el usuario cancela, salir

            string nuevoAsiento = await DisplayPromptAsync("Editar Boleto", "Ingrese el nuevo número de asiento:",
                initialValue: boleto.Asiento);
            if (nuevoAsiento == null) return;  // Si el usuario cancela, salir

            string nuevaFechaViaje = await DisplayPromptAsync("Editar Boleto",
                "Ingrese la nueva fecha de viaje (YYYY-MM-DD):",
                initialValue: boleto.FechaViaje.ToString("yyyy-MM-dd"));
            if (nuevaFechaViaje == null) return;  // Si el usuario cancela, salir

            string nuevoPrecio = await DisplayPromptAsync("Editar Boleto", "Ingrese el nuevo precio del boleto:",
                initialValue: boleto.Precio.ToString());
            if (nuevoPrecio == null) return;  // Si el usuario cancela, salir

            string nuevoEstadoReserva = await DisplayPromptAsync("Editar Boleto",
                "Ingrese el nuevo estado de la reserva:", initialValue: boleto.EstadoReserva);
            if (nuevoEstadoReserva == null) return;  // Si el usuario cancela, salir

            // Validar que los campos no estén vacíos
            if (string.IsNullOrWhiteSpace(nuevoNombrePasajero) || string.IsNullOrWhiteSpace(nuevoAsiento) ||
                string.IsNullOrWhiteSpace(nuevaFechaViaje) || string.IsNullOrWhiteSpace(nuevoPrecio) ||
                string.IsNullOrWhiteSpace(nuevoEstadoReserva))
            {
                await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
                return;
            }

            if (!int.TryParse(nuevoAsiento, out int asientoParsed))
            {
                await DisplayAlert("Error", "El número de asiento debe ser un entero válido.", "OK");
                return;
            }

            // Actualizar los datos del objeto boleto
            boleto.NombrePasajero = nuevoNombrePasajero;
            boleto.Asiento = asientoParsed.ToString();
            boleto.FechaViaje = DateTime.Parse(nuevaFechaViaje);
            boleto.Precio = decimal.Parse(nuevoPrecio);
            boleto.EstadoReserva = nuevoEstadoReserva;

            // Serializar el objeto actualizado
            var client = new HttpClient();
            var json = JsonSerializer.Serialize(boleto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Enviar solicitud PUT al API
            var response = await client.PutAsync($"http://localhost:5191/api/Boletos/editar/{boleto.ReservaId}", content);

            if (response.IsSuccessStatusCode)
            {
                await DisplayAlert("Éxito", "El boleto se actualizó correctamente.", "OK");

                // Recargar la lista para reflejar los cambios
                CargarTodosLosBoletos();
            }
            else
            {
                await DisplayAlert("Error", "No se pudo actualizar el boleto en el API.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }

    // Cambiar el método del botón de "Actualizar" a usar EditarBoleto
    private void OnActualizarClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Boleto boleto)
        {
            EditarBoleto(boleto);
        }
    }


    // Método para eliminar un boleto
    private async void EliminarBoleto(int reservaId)
    {
        // Mostrar ventana de confirmación antes de eliminar
        bool respuesta = await DisplayAlert("Confirmación",
                                             "¿Estás seguro de eliminar este boleto?",
                                             "Sí", "No");

        if (respuesta) // Si el usuario presiona "Sí"
        {
            try
            {
                var client = new HttpClient();
                var response = await client.DeleteAsync($"http://localhost:5191/api/Boletos/eliminar/{reservaId}");

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Éxito", "El boleto se eliminó correctamente.", "OK");
                    CargarTodosLosBoletos(); // Recargar los boletos
                }
                else
                {
                    await DisplayAlert("Error", "No se pudo eliminar el boleto.", "OK");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "OK");
            }
        }
        else
        {
            // Si el usuario presiona "No", no hace nada y se sale
            await DisplayAlert("Cancelado", "El boleto no fue eliminado.", "OK");
        }
    }


    // Event handlers para los botones
    private void EditarBoleto(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Boleto boleto)
        {
            EditarBoleto(boleto);
        }
    }

    private void OnEliminarClicked(object sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is Boleto boleto)
        {
            EliminarBoleto(boleto.ReservaId);
        }
    }


    private async void OnCrearBoletoClicked(object sender, EventArgs e)
    {
   
        // Mostrar formulario para crear boleto

        // Se valida que el usuario no cancele en ningún paso
        string nombrePasajero = await DisplayPromptAsync("Crear Boleto", "Ingrese el nombre del pasajero:");
        if (nombrePasajero == null) return;  // Si el usuario cancela, salir del método

        string asiento = await DisplayPromptAsync("Crear Boleto", "Ingrese el número de asiento:");
        if (asiento == null) return;  // Si el usuario cancela, salir del método

        string fechaViaje = await DisplayPromptAsync("Crear Boleto", "Ingrese la fecha de viaje (YYYY-MM-DD):");
        if (fechaViaje == null) return;  // Si el usuario cancela, salir del método

        string precio = await DisplayPromptAsync("Crear Boleto", "Ingrese el precio del boleto:");
        if (precio == null) return;  // Si el usuario cancela, salir del método

        string estadoReserva = await DisplayPromptAsync("Crear Boleto", "Ingrese el estado de la reserva:");
        if (estadoReserva == null) return;  // Si el usuario cancela, salir del método

        // Verificación de campos vacíos
        if (string.IsNullOrWhiteSpace(nombrePasajero) ||
            string.IsNullOrWhiteSpace(asiento) ||
            string.IsNullOrWhiteSpace(fechaViaje) ||
            string.IsNullOrWhiteSpace(precio) ||
            string.IsNullOrWhiteSpace(estadoReserva))
        {
            await DisplayAlert("Error", "Todos los campos son obligatorios.", "OK");
            return;
        }

        try
        {
            // Validación de formato del asiento
            if (!int.TryParse(asiento, out int asientoParsed))
            {
                await DisplayAlert("Error", "El valor ingresado para el número de asiento no es válido. Debe ser un número entero.", "OK");
                return;
            }

            // Crear objeto de boleto
            var newBoleto = new Boleto
            {
                NombrePasajero = nombrePasajero,
                Asiento = asientoParsed.ToString(),
                FechaViaje = DateTime.Parse(fechaViaje),
                Precio = decimal.Parse(precio),
                EstadoReserva = estadoReserva
            };

            // Serializar objeto a JSON
            var json = JsonSerializer.Serialize(newBoleto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            // Enviar solicitud POST al API
            var client = new HttpClient();
            var response = await client.PostAsync("http://localhost:5191/api/Boletos/crear", content);

            if (response.IsSuccessStatusCode)
            {
                // Obtener boleto creado desde la respuesta del API
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var boletoCreado = JsonSerializer.Deserialize<Boleto>(jsonResponse, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                // Agregar boleto a la lista local
                if (BindingContext is BoletoViewModel viewModel && boletoCreado != null)
                {
                    viewModel.Boletos.Add(boletoCreado);
                }

                await DisplayAlert("Éxito", "El boleto fue creado correctamente.", "OK");
            }
            else
            {
                await DisplayAlert("Error", "No se pudo crear el boleto en el API.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Ocurrió un error: {ex.Message}", "OK");
        }
    }


    private async void OnActualizarListaClicked(object sender, EventArgs e)
    {
        CargarTodosLosBoletos(); // Llamar al método de recarga
    }
}