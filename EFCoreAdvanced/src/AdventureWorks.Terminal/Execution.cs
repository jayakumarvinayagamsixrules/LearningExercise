
using AdventureWorks.Infrastructure.AdventureWorksRepositories;
using BenchmarkDotNet.Attributes;
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

        var count = await _mediator.Send(new GetUserCountQuery { NameFilter = "Rebecca" });
        Console.WriteLine($"Found {count} entries");        

    }
}

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
        var sales = await _salesRepository.GetSalesAsync();
        foreach (var sale in sales)
        {
            Console.WriteLine($"Id: {sale.Id}, {sale.LineTotal} ({sale.UnitPrice})");
        }
        return await Task.FromResult(1000);
    }
}