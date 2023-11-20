namespace CargoApp.Domain.Entities;

/// <summary>
/// Перечисление, представляющее возможные статусы курьера.
/// </summary>
public enum CourierStatus
{
    /// <summary>
    /// Курьер доступен
    /// </summary>
    Free,

    /// <summary>
    /// Курьер недоступен
    /// </summary>
    Busy,
    
    /// <summary>
    /// Курьер доставляет груз
    /// </summary>
    InDelivery
}