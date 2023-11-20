using CargoApp.Application.DTOs;

namespace CargoApp.Application.Services.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления заявками.
/// </summary>
public interface ICargoService
{
    /// <summary>
    /// Вызывает метод репозитория для получения заявок.
    /// </summary>
    /// <returns>Список заявок.</returns>
    Task<IEnumerable<CargoDto>> GetCargoRequestsAsync();

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Cписок заявок </returns>
    Task<IEnumerable<CargoDto>> SearchCargoRequestsAsync(string searchText, bool caseSensitive);

    /// <summary>
    /// Вызывает метод репозитория для получения заявки.
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    /// <returns>Найденную заявку или null</returns>
    Task<CargoDto?> GetCargoRequestAsync(int id);

    /// <summary>
    /// Вызывает метод репозитория для редактирования заявки.
    /// </summary>
    /// <param name="cargo">заявка для редактирования.</param>
    Task EditCargoRequest(CargoDto cargo);

    /// <summary>
    /// Вызывает метод репозитория для создания заявки.
    /// </summary>
    /// <param name="cargo">Заявка.</param>
    Task CreateCargoRequest(CargoDto cargo);

    /// <summary>
    /// Вызывает метод репозитория для удаления заявки.
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    Task DeleteCargoRequest(int id);

    /// <summary>
    /// Переводит заявку в статус "Передано на выполнение."
    /// </summary>
    /// <param name="id">Идентификатор заявки.</param>
    Task TransitCargoRequest(int id);
}