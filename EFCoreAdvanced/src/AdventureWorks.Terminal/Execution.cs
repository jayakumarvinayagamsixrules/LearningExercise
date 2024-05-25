
using System.Diagnostics;
using AdventureWorks.Domain.Models;
using AdventureWorks.Domain.Sales.Queries;
using AdventureWorks.Infrastructure.AdventureWorksDbContext;
using AdventureWorks.Infrastructure.AdventureWorksRepositories;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using MediatR;

namespace AdventureWorks.Terminal;
public class Execution
{
    private readonly IMediator _mediator;

    public Execution(IMediator mediator)
    {
        _mediator = mediator;
    }
    
     public async Task RunAsync()
    {
        await Task.Delay(0);
        Console.WriteLine("Hai AdventureWorks!");

        //BenchmarkRunner.Run<GetUserCountQueryHandlerBenchmark>();
        var count = await _mediator.Send(new GetUserCountQuery { NameFilter = "Rebecca" });
        Console.WriteLine($"Found {count} entries");        
        // var saleOrders = await _mediator.Send(new GetSalesOrderQuery());
        // Console.WriteLine($"Sales Order Count: {saleOrders.SalesOrders.Count()}");
    }
}


public interface IQuery<out TResponse> : IRequest<TResponse>  
    where TResponse : notnull
{
}
public interface IQueryHandler<in TQuery, TResponse>
    : IRequestHandler<TQuery, TResponse>
    where TQuery : IQuery<TResponse>
    where TResponse : notnull
{
}

// public record GetSalesOrderQuery() : IQuery<GetSalesOrderResult>;
// public record GetSalesOrderResult(ICollection<SalesOrder> SalesOrders);
// public class GetSalesOrderQueryHandler(ISalesRepository salesRepository) : IQueryHandler<GetSalesOrderQuery, GetSalesOrderResult>
// {
//     public async Task<GetSalesOrderResult> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
//     {
//         var saleOrders = await salesRepository.GetSalesAsync();
//         return new GetSalesOrderResult(saleOrders);
//     }
// }

public class GetUserCountQuery : IRequest<int>
{
    public string? NameFilter { get; set; }
}
public class GetUserCountQueryHandler : IRequestHandler<GetUserCountQuery, int>
{
    private readonly ISalesRepository _salesRepository;

    public GetUserCountQueryHandler(ISalesRepository salesRepository)
    {
        _salesRepository = salesRepository;
    }

    public async Task<int> Handle(GetUserCountQuery request, CancellationToken cancellationToken)
    {     
        
        Stopwatch stopwatch = new Stopwatch();
        stopwatch.Start();               
        var sales = await _salesRepository.GetSalesAsync();
        stopwatch.Stop();
        TimeSpan elapsed = stopwatch.Elapsed;
        Console.WriteLine("Execution time: " + elapsed.TotalMilliseconds + "ms");     
        // foreach (var sale in sales)
        // {
        //     Console.WriteLine($"Id: {sale.Id}, {sale.LineTotal} ({sale.UnitPrice})");
        // }
        return await Task.FromResult(1000);
    }
}


[MemoryDiagnoser]
public class GetUserCountQueryHandlerBenchmark
{

    private readonly ISalesRepository _salesRepository;

    public GetUserCountQueryHandlerBenchmark()
    {
        _salesRepository = new SalesRepository(new AdventureWorks2019Context());
    }
    [Benchmark(Baseline = true)]
    public void Execution()
    {

    }       
}