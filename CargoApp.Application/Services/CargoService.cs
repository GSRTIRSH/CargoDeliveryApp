using CargoApp.Application.DTOs;
using CargoApp.Application.Services.Interfaces;
using CargoApp.Domain.Entities;
using CargoApp.Domain.Repositories.Interfaces;
using Microsoft.Extensions.Logging;

//using Microsoft.Extensions.Logging;

namespace CargoApp.Application.Services;

public class CargoService : ICargoService
{
    private readonly ICargoRepository _cargoRepository;
    private readonly ILogger<CargoService> _logger;

    public CargoService(ICargoRepository cargoRepository, ILogger<CargoService> logger)
    {
        _cargoRepository = cargoRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<CargoDto>> GetCargoRequestsAsync()
    {
        try
        {
            return (await _cargoRepository.GetCargoRequestsAsync()).Select(t => new CargoDto()
            {
                Id = t.Id,
                PickupDateTime = t.PickupDateTime,
                PickupLocation = t.PickupLocation,
                DeliveryLocation = t.DeliveryLocation,
                Status = t.Status,
                CancellationReason = t.CancellationReason,
            });
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while getting list of cargo request: {message}", e.Message);
            return new List<CargoDto>();
        }
    }

    public async Task<IEnumerable<CargoDto>> SearchCargoRequestsAsync(string searchText, bool caseSensitive)
    {
        try
        {
            return (await _cargoRepository.SearchCargoRequestsAsync(searchText, caseSensitive)).Select(t =>
                new CargoDto
                {
                    Id = t.Id,
                    PickupDateTime = t.PickupDateTime,
                    PickupLocation = t.PickupLocation,
                    DeliveryLocation = t.DeliveryLocation,
                    Status = t.Status,
                    CancellationReason = t.CancellationReason,
                });
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while searching for cargo requests: {message}", e.Message);
            return new List<CargoDto>();
        }
    }

    public async Task<CargoDto?> GetCargoRequestAsync(int id)
    {
        try
        {
            var t = await _cargoRepository.GetCargoRequestAsync(id);
            var cargoDto = new CargoDto()
            {
                Id = t.Id,
                PickupDateTime = t.PickupDateTime,
                PickupLocation = t.PickupLocation,
                DeliveryLocation = t.DeliveryLocation,
                Status = t.Status,
                CancellationReason = t.CancellationReason,
            };
            return cargoDto;
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while getting cargo request: {message}", e.Message);
            return null;
        }
    }

    public async Task EditCargoRequest(CargoDto cargoDto)
    {
        try
        {
            var cargo = new Cargo()
            {
                Id = cargoDto.Id,
                ClientId = Guid.NewGuid(),
                PickupLocation = cargoDto.PickupLocation,
                DeliveryLocation = cargoDto.DeliveryLocation,
                PickupDateTime = cargoDto.PickupDateTime,
                Status = cargoDto.Status,
                CancellationReason = cargoDto.CancellationReason ?? ""
            };
            await _cargoRepository.EditCargoRequestAsync(cargo);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while editing cargo request: {message}", e.Message);
        }
    }

    public async Task CreateCargoRequest(CargoDto cargoDto)
    {
        try
        {
            var cargo = new Cargo()
            {
                Id = cargoDto.Id,
                ClientId = Guid.NewGuid(),
                PickupLocation = cargoDto.PickupLocation,
                DeliveryLocation = cargoDto.DeliveryLocation,
                PickupDateTime = cargoDto.PickupDateTime,
                Status = cargoDto.Status,
                CancellationReason = cargoDto.CancellationReason ?? ""
            };
            await _cargoRepository.CreateCargoRequestAsync(cargo);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while creating cargo request: {message}", e.Message);
        }
    }

    public async Task DeleteCargoRequest(int id)
    {
        try
        {
            await _cargoRepository.DeleteCargoRequestAsync(id);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while deleting cargo request: {message}", e.Message);
        }
    }

    public async Task TransitCargoRequest(int id)
    {
        try
        {
            // логика передачи заявки курьеру
            var cargo = _cargoRepository.GetCargoRequestAsync(id).Result;
            cargo.Status = CargoStatus.InTransit;
            await _cargoRepository.EditCargoRequestAsync(cargo);
        }
        catch (Exception e)
        {
            _logger.LogError("An error occurred while transiting cargo request: {message}", e.Message);
        }
    }
}