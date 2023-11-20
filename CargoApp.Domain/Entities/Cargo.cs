namespace CargoApp.Domain.Entities;

public class Cargo
{
    public int Id { get; set; }

    public Guid ClientId { get; set; }

    public string PickupLocation { get; set; }

    public string DeliveryLocation { get; set; }

    public DateTime PickupDateTime { get; set; }

    public CargoStatus Status { get; set; }

    public string CancellationReason { get; set; }

    public Guid? CourierId { get; set; }

    public Courier Courier { get; set; }
}