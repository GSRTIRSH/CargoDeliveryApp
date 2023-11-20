namespace CargoApp.Domain.Entities;

/// <summary>
/// Перечисление, представляющее возможные статусы груза.
/// </summary>
public enum CargoStatus
{
    /// <summary>
    /// Новая заявка на груз.
    /// </summary>
    New,

    /// <summary>
    /// Груз передан на выполнение курьерами.
    /// </summary>
    InTransit,

    /// <summary>
    /// Груз успешно доставлен.
    /// </summary>
    Delivered,

    /// <summary>
    /// Заявка на груз отменена.
    /// </summary>
    Canceled,
}

/// <summary>
/// Статический класс, предоставляющий локализацию для статусов груза.
/// </summary>
public static class CargoStatusName
{
    /// <summary>
    /// Имя для статуса "Новая".
    /// </summary>
    public const string New = "Новая";

    /// <summary>
    /// Имя для статуса "Передано на выполнение".
    /// </summary>
    public const string InTransit = "Передано на выполнение";

    /// <summary>
    /// Имя для статуса "Выполнено".
    /// </summary>
    public const string Delivered = "Выполнено";

    /// <summary>
    /// Имя для статуса "Отменена".
    /// </summary>
    public const string Canceled = "Отменена";

    /// <summary>
    /// Получает локализованное имя для заданного статуса груза.
    /// </summary>
    /// <param name="status">Статус груза.</param>
    /// <returns>Локализованное имя для указанного статуса груза.</returns>
    public static string GetName(this CargoStatus status) => status switch
    {
        CargoStatus.New => New,
        CargoStatus.InTransit => InTransit,
        CargoStatus.Delivered => Delivered,
        CargoStatus.Canceled => Canceled,
    };
}