using CargoApp.Domain.Entities;

namespace CargoApp.Application.DTOs;

public class CargoDto
{
    public int Id { get; set; }
    
    public string PickupLocation { get; set; }

    public string DeliveryLocation { get; set; }

    public DateTime PickupDateTime { get; set; }

    public CargoStatus Status { get; set; }

    public string? CancellationReason { get; set; }
}