namespace Orders.Services.Orders.Enum;

/// <summary>
/// Represents the status of the order.
/// </summary>
public enum OrderStatus
{
    /// <summary>
    /// The order is active.
    /// </summary>
    Pending = 1,

    /// <summary>
    /// The order was cancelled.
    /// </summary>
    Cancelled = 2,

    /// <summary>
    /// The order was completed.
    /// </summary>
    Paid = 3
}
