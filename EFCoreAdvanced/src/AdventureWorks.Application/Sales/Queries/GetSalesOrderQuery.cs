
using AdventureWorks.Domain.Models;
using AdventureWorks.Infrastructure.AdventureWorksRepositories;
using MediatR;

namespace AdventureWorks.Domain.Sales.Queries;

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

public record GetSalesOrderQuery() : IQuery<GetSalesOrderResult>;
public record GetSalesOrderResult(ICollection<SalesOrder> SalesOrders);
public class GetSalesOrderQueryHandler(ISalesRepository salesRepository) : IQueryHandler<GetSalesOrderQuery, GetSalesOrderResult>
{
    public async Task<GetSalesOrderResult> Handle(GetSalesOrderQuery request, CancellationToken cancellationToken)
    {
        var saleOrders = await salesRepository.GetSalesAsync();
        return new GetSalesOrderResult(saleOrders);
    }
}



/**

public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;
public record GetBasketResult(ShoppingCart Cart);

public class GetBasketQueryHandler(IBasketRepository repository)
    : IQueryHandler<GetBasketQuery, GetBasketResult>
{
    public async Task<GetBasketResult> Handle(GetBasketQuery query, CancellationToken cancellationToken)
    {
        var basket = await repository.GetBasket(query.UserName);

        return new GetBasketResult(basket);
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
*/