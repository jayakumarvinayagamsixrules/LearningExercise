namespace AdventureWorks.Domain.Models;

public record SalesOrder(
    int Id,
    int DetailId,
    string? CarrierTrackingNumber,
    short OrderQty,
    int ProductId,
    decimal UnitPrice,
    decimal UnitPriceDiscount,
    decimal LineTotal
);