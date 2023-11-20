using CargoApp.Domain.Entities;

namespace CargoApp.Domain.Repositories.Interfaces;

/// <summary>
/// Интерфейс репозитория грузовых заявок.
/// </summary>
public interface ICargoRepository
{
    /// <summary>
    /// Получает список всех грузовых заявок.
    /// </summary>
    /// <returns>Список заявок.</returns>
    Task<IEnumerable<Cargo>> GetCargoRequestsAsync();
    
    /// <summary>
    /// Поиск грузовых заявок по текстовому запросу.
    /// </summary>
    /// <param name="searchText">Текст для поиска по заявкам.</param>
    /// <param name="caseSensitive">Учитывать ли регистр при поиске по заявкам.</param>
    /// <returns>Список заявок, соответствующих текстовому запросу.</returns>
    Task<IEnumerable<Cargo>> SearchCargoRequestsAsync(string searchText, bool caseSensitive);

    /// <summary>
    /// Получает заявку по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор грузовой заявки.</param>
    /// <returns>Заявка</returns>
    /// <exception cref="ArgumentNullException">если заявка не найдена.</exception>
    Task<Cargo> GetCargoRequestAsync(int id);

    /// <summary>
    /// Создает новую заявку.
    /// </summary>
    /// <param name="cargoRequest">Заявка для создания.</param>
    Task CreateCargoRequestAsync(Cargo cargoRequest);

    /// <summary>
    /// Редактирует существующую заявку.
    /// </summary>
    /// <param name="cargoRequest">Грузовая заявка для редактирования.</param>
    Task EditCargoRequestAsync(Cargo cargoRequest);

    /// <summary>
    /// Удаляет грузовую заявку по идентификатору.
    /// </summary>
    /// <param name="cargoRequestId">Идентификатор грузовой заявки для удаления.</param>
    Task DeleteCargoRequestAsync(int cargoRequestId);
}