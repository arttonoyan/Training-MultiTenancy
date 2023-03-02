using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Training.MultiTenancy.Data;

using IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((context, config) => {
        config.AddJsonFile("appsettingsTools.json", true, true);
        config.AddEnvironmentVariables();
    })
    .ConfigureServices((context, services) => {
        services.AddMultiTenancyData(context.Configuration.GetConnectionString("LocalDbConnection")!, Console.WriteLine);
    })
    .Build();

var dbContext = host.Services.GetRequiredService<TenantDbCntext>();
await dbContext.Database.MigrateAsync();

//await host.RunAsync();

// Info:
// https://learn.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs
//Open "Package Manager Console": Tools->NuGet Package Manager->Package Manager Console

/* CREATE NEW MIGRATION
 * 1) Set Tools Project as Startup Project
 * 2) In Package Manager Consloe Window choose Default project: Training.MultiTenancy.Data
 * 3) Add-Migration {Migration Name} -OutputDir Infrastructure\Migrations
 * 4) Update-Database
 */

/* UPDATE DATABASE WITH CODE
 * 1) Set Tools Project as Startup Project
 * 3) Run Project
 */

/* UPDATE DATABASE WITH COMMAND LINE
 * 1) Set Tools Project as Startup Project
 * 2) In "Package Manager Console" Window choose Default project: Training.MultiTenancy.Data
 * 3) CMD: Update-Database
 */
