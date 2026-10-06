using System.Net.Http.Json;
using LogiTrack.OrderService.Application.DTOs.Shipments;
using LogiTrack.OrderService.Application.Interfaces;

namespace LogiTrack.OrderService.Infrastructure.Clients;

public class ShipmentClient : IShipmentClient
{
    private readonly HttpClient _httpClient;

    public ShipmentClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<CreateShipmentResponse?> CreateShipmentAsync(
     CreateShipmentRequest request)
    {
        try
        {
            var response = await _httpClient.PostAsJsonAsync(
                "api/Shipments",
                request);

            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();

                throw new HttpRequestException(
                    $"Shipment Service returned " +
                    $"HTTP {(int)response.StatusCode} " +
                    $"{response.StatusCode}. " +
                    $"Response: {errorContent}");
            }

            return await response.Content
                .ReadFromJsonAsync<CreateShipmentResponse>();
        }
        catch (TaskCanceledException ex)
        {
            throw new TimeoutException(
                "Shipment Service request timed out.",
                ex);
        }
    }
}