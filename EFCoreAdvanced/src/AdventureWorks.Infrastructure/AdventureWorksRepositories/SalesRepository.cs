
using AdventureWorks.Domain.Models;
using AdventureWorks.Infrastructure.AdventureWorksDbContext;
using BenchmarkDotNet.Attributes;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AdventureWorks.Infrastructure.AdventureWorksRepositories;

public interface ISalesRepository 
{
    Task<ICollection<SalesOrder>> GetSalesAsync();
}

public class SalesRepository(AdventureWorks2019Context adventureWorks2019Context) : ISalesRepository
{   
    public async Task<ICollection<SalesOrder>> GetSalesAsync()
    {
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();               
        var tt = await adventureWorks2019Context.SalesOrderHeaders
            .Include(x=> x.SalesOrderHeaderSalesReasons)
            .AsSplitQuery()
            .ToListAsync();
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        Console.WriteLine("Execution time ===>>> (SalesOrderHeaders): " + elapsed.TotalMilliseconds + "ms");   

        return await adventureWorks2019Context.SalesOrderDetails
        .Take(1000)
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