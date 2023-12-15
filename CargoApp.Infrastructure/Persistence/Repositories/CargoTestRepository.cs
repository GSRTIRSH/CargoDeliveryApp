using CargoApp.Domain.Entities;
using CargoApp.Domain.Repositories.Interfaces;

namespace CargoApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация репозитория с хранением в памяти приложения
/// </summary>
public class CargoTestRepository : ICargoRepository
{
    private readonly List<Cargo> _сargoList = new();

    public async Task<IEnumerable<Cargo>> GetCargoRequestsAsync()
    {
        return await Task.Run(() => _сargoList);
    }

    public Task<Cargo> GetCargoRequestAsync(int id)
    {
        return Task.Run(() =>
        {
            return _сargoList.Find(c => c.Id == id) ?? throw new ArgumentException(id.ToString());
        });
    }

    public Task CreateCargoRequestAsync(Cargo cargoRequest)
    {
        return Task.Run(() =>
        {
            cargoRequest.Id = _сargoList.LastOrDefault()?.Id + 1 ?? 0;
            _сargoList.Add(cargoRequest);
        });
    }

    public Task EditCargoRequestAsync(Cargo cargoRequest)
    {
        return Task.Run(() =>
        {
            var edit = _сargoList.Find(t => t.Id == cargoRequest.Id);
            if (edit == null) return;

            edit.Status = cargoRequest.Status;
            edit.CancellationReason = cargoRequest.CancellationReason;
            if (edit.Status != CargoStatus.New) return;

            edit.PickupLocation = cargoRequest.PickupLocation;
            edit.DeliveryLocation = cargoRequest.DeliveryLocation;
            edit.PickupDateTime = cargoRequest.PickupDateTime;
        });
    }

    public Task DeleteCargoRequestAsync(int cargoRequestId)
    {
        return Task.Run(() =>
        {
            var remove = _сargoList.Find(t => t.Id == cargoRequestId);
            if (remove != null) _сargoList.Remove(remove);
        });
    }

    public async Task<IEnumerable<Cargo>> SearchCargoRequestsAsync(string searchText, bool caseSensitive)
    {
        var comparison = caseSensitive
            ? StringComparison.CurrentCulture
            : StringComparison.CurrentCultureIgnoreCase;

        return await Task.Run(() =>
        {
            return _сargoList
                .Where(request =>
                    request.PickupLocation.Contains(searchText, comparison) ||
                    request.DeliveryLocation.Contains(searchText, comparison) ||
                    request.PickupDateTime.ToString("yyyy-MM-dd HH:mm").Contains(searchText, comparison) ||
                    request.Status.GetName().Contains(searchText, comparison) ||
                    request.CancellationReason.Contains(searchText, comparison))
                .ToList();
        });
    }
}