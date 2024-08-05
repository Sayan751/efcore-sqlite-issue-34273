using System.Data.Common;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace efcore_sqlite_issue_34273.Data;

public class DatabaseConnectionInterceptor : DbConnectionInterceptor
{
	public override void ConnectionFailed(DbConnection connection, ConnectionErrorEventData eventData)
	{
		base.ConnectionFailed(connection, eventData);
		Log(eventData.Exception);
	}

	public override async Task ConnectionFailedAsync(DbConnection connection, ConnectionErrorEventData eventData, CancellationToken cancellationToken = default)
	{
		await base.ConnectionFailedAsync(connection, eventData, cancellationToken);
		Log(eventData.Exception);
	}

	private static void Log(Exception exception)
	{
		Console.WriteLine("===============================================");
		Console.WriteLine(exception);
		Console.WriteLine("===============================================");
	}
}
