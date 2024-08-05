using System.Reflection;
using efcore_sqlite_issue_34273.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
	services.AddDbContextPool<DatabaseContext>((serviceProvider, options) =>
	{
		var cwd = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
		options.LogTo(Console.WriteLine);
		options.UseSqlite($"Data Source={Path.Combine(cwd, $"data-{DateTime.UtcNow:yyyy-MM-ddTHH-mm-ssZ}.db")}");
		options.AddInterceptors(serviceProvider.GetRequiredService<DatabaseConnectionInterceptor>());
	})
	.AddPooledDbContextFactory<DatabaseContext>(options => { })
	.AddSingleton<DatabaseConnectionInterceptor>();
});

var host = builder.Build();

Console.WriteLine("Migrating database...");
await MigrateAsync(host.Services);
Console.WriteLine("Database migrated.");

static async Task MigrateAsync(IServiceProvider serviceProvider)
{
	var ctxFactory = serviceProvider.GetRequiredService<IDbContextFactory<DatabaseContext>>();
	await using var ctx = await ctxFactory.CreateDbContextAsync();
	await ctx.Database.MigrateAsync();
}
