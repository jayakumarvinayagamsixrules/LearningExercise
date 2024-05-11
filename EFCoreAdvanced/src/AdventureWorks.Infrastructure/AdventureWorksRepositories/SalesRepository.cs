
using AdventureWorks.Domain.Models;
using AdventureWorks.Infrastructure.AdventureWorksDbContext;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;

namespace AdventureWorks.Infrastructure.AdventureWorksRepositories;

public interface ISalesRepository 
{
    Task<ICollection<SalesOrder>> GetSalesAsync();
}

public class SalesRepository(AdventureWorks2019Context adventureWorks2019Context) : ISalesRepository
{   
    public async Task<ICollection<SalesOrder>> GetSalesAsync()
    {
        return await adventureWorks2019Context.SalesOrderDetails.Take(10)
        .Select( x => new SalesOrder(
            x.SalesOrderId, 
            x.SalesOrderDetailId,
            x.CarrierTrackingNumber,
            x.OrderQty, 
            x.ProductId, 
            x.UnitPrice, 
            x.UnitPriceDiscount, 
            x.LineTotal)).ToListAsync();
    }
}