
using AdventureWorks.Infrastructure.AdventureWorksDbContext;
using AdventureWorks.Infrastructure.AdventureWorksRepositories;
using AdventureWorks.Terminal;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Environments;
using BenchmarkDotNet.Jobs; 
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

var applicationSettingBuilder = new ConfigurationBuilder()
                 .AddJsonFile($"appsettings.json", true, true)
                 .Build();
var serviceProvider = CreateServiceProvider();
await serviceProvider.GetRequiredService<Execution>().RunAsync();

Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();
Log.Information("Ah, there you are!");

// static IConfig GetGlobalConfig()
//             => DefaultConfig.Instance
//             .AddJob(CoreRuntime.Core60);
//BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly).Run(args);
// BenchmarkSwitcher.FromAssembly(typeof(Program).Assembly)
//                             .Run(args, GetGlobalConfig());

ServiceProvider CreateServiceProvider()
{
    var collection = new ServiceCollection();
    // This will execute our main logic
    collection.AddScoped<Execution>();
    
    // This comes from the MediatR package.
    // It looks for all commands, queries and handlers and registers
    // them in the container
    collection.AddMediatR(typeof(Program).Assembly);
    
    // Our repository

    collection.AddDbContext<AdventureWorks2019Context>(
        options => options.UseSqlServer(applicationSettingBuilder["ConnectionString"])
        //.LogTo(Console.WriteLine)
        .LogTo(Console.WriteLine, LogLevel.Information)
        .EnableDetailedErrors()
        //.LogTo(new EFCoreFileLog().Log)
    );

    collection.AddSingleton<AdventureWorks2019Context>();
    collection.AddScoped<ISalesRepository, SalesRepository>();
    return collection.BuildServiceProvider();
}

public static class ConfigExtensions
    {
        public static ManualConfig AddJob(this IConfig config, Runtime runtime)
        {
            return config.AddJob(
                Job.Default
                    .WithWarmupCount(2)
                    .WithIterationCount(20)
                    .WithRuntime(runtime)
                );
        }
    }