using CargoApp.Domain.Entities;
using CargoApp.Domain.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Infrastructure.Persistence.Repositories;

/// <summary>
/// Реализация репозитория с хранением в базе данных
/// </summary>
public class CargoRepository : ICargoRepository
{
    private readonly CargoDbContext _context;

    public CargoRepository(CargoDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Cargo>> GetCargoRequestsAsync()
    {
        return await _context.CargoRequests.OrderBy(t => t.Id).ToListAsync();
    }

    public async Task<Cargo> GetCargoRequestAsync(int id)
    {
        return await _context.CargoRequests.SingleAsync(c => c.Id == id);
    }

    public async Task CreateCargoRequestAsync(Cargo cargoRequest)
    {
        cargoRequest.PickupDateTime = cargoRequest.PickupDateTime.ToUniversalTime();
        await _context.CargoRequests.AddAsync(cargoRequest);
        await _context.SaveChangesAsync();
    }

    public async Task EditCargoRequestAsync(Cargo cargoRequest)
    {
        cargoRequest.PickupDateTime = cargoRequest.PickupDateTime.ToUniversalTime();

        // Обновляем все поля только если заявка находится в статусе новая, иначе сам статус
        if (cargoRequest.Status == CargoStatus.New)
            _context.CargoRequests.Update(cargoRequest);
        else
        {
            _context.CargoRequests.Attach(cargoRequest);
            _context.Entry(cargoRequest).Property(x => x.Status).IsModified = true;
            _context.Entry(cargoRequest).Property(x => x.CancellationReason).IsModified = true;
        }

        await _context.SaveChangesAsync();
    }

    public async Task DeleteCargoRequestAsync(int cargoRequestId)
    {
        var cargoRequest = await _context.CargoRequests.FindAsync(cargoRequestId);
        if (cargoRequest == null) return;
        _context.CargoRequests.Remove(cargoRequest);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<Cargo>> SearchCargoRequestsAsync(string searchText, bool caseSensitive)
    {
        var comparison = caseSensitive
            ? StringComparison.InvariantCulture
            : StringComparison.InvariantCultureIgnoreCase;

        // Поиск по всем полям приходится делать в памяти, а не в базе данных, из-за необходимости преобразовывать столбцы 
        return _context.CargoRequests.ToList()
            .Where(request =>
                request.PickupLocation.Contains(searchText, comparison) ||
                request.DeliveryLocation.Contains(searchText, comparison) ||
                request.PickupDateTime.ToString("yyyy-MM-dd HH:mm").Contains(searchText, comparison) ||
                request.Status.GetName().Contains(searchText, comparison) ||
                request.CancellationReason.Contains(searchText, comparison))
            .ToList();
    }
}