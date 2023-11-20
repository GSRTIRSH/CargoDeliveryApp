using CargoApp.Domain.Entities;
using CargoApp.Domain.Repositories.Interfaces;

namespace CargoApp.Infrastructure.Persistence;

public class DataInitializer
{
    private readonly List<Cargo> _cargoList = new()
    {
        new Cargo
        {
            Id = 1,
            PickupLocation = "Main Street 1, Cityville",
            DeliveryLocation = "Broadway Avenue 1, Townburg",
            PickupDateTime = DateTime.Now.AddMinutes(30).ToUniversalTime(),
            Status = CargoStatus.InTransit,
            CancellationReason = ""
        },
        new Cargo
        {
            Id = 2,
            PickupLocation = "Oak Avenue 2, Villagetown",
            DeliveryLocation = "Maple Lane 2, Hamletsville",
            PickupDateTime = DateTime.Now.AddDays(1).ToUniversalTime(),
            Status = CargoStatus.Delivered,
            CancellationReason = ""
        },
        new Cargo
        {
            Id = 3,
            PickupLocation = "Elm Street 3, Metropolis",
            DeliveryLocation = "Pine Boulevard 3, Megatown",
            PickupDateTime = DateTime.Now.AddDays(1).AddHours(3).ToUniversalTime(),
            Status = CargoStatus.New,
            CancellationReason = ""
        },
        new Cargo
        {
            Id = 4,
            PickupLocation = "Cedar Road 4, Urbancity",
            DeliveryLocation = "Spruce Court 4, Suburbville",
            PickupDateTime = DateTime.Now.AddDays(1).AddHours(6).AddMinutes(30).ToUniversalTime(),
            Status = CargoStatus.Canceled,
            CancellationReason = "Причина отказа"
        }
    };

    private readonly List<Courier> _courierList = new()
    {
        new Courier
        {
            Id = Guid.NewGuid(),
            Name = "John Doe",
            CargoInDelivery = new List<Cargo>(),
            CourierStatus = CourierStatus.Free
        },
        new Courier
        {
            Id = Guid.NewGuid(),
            Name = "Jane Smith",
            CargoInDelivery = new List<Cargo>(),
            CourierStatus = CourierStatus.Busy
        },
        new Courier
        {
            Id = Guid.NewGuid(),
            Name = "Bob Johnson",
            CargoInDelivery = new List<Cargo>(),
            CourierStatus = CourierStatus.InDelivery
        }
    };

    private readonly ICargoRepository _cargoRepository;

    public DataInitializer(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }

    public async void InitializeData()
    {
        if (_cargoRepository.GetCargoRequestsAsync().Result.Any()) return;

        foreach (var cargo in _cargoList)
        {
           await _cargoRepository.CreateCargoRequestAsync(cargo);
        }
    }
}