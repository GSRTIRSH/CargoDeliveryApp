namespace CargoApp.Domain.Entities;

public class Courier
{
    public Guid Id { get; set; }
    
    public string Name { get; set; }

    public List<Cargo> CargoInDelivery { get; set; }

    public CourierStatus CourierStatus { get; set; }
}