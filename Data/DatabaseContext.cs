using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace efcore_sqlite_issue_34273.Data;

internal class DatabaseContext : DbContext
{
	public DbSet<Foo> Foos { get; set; }
	public DbSet<Foo2> Foo2s { get; set; }
	public DbSet<Foo3> Foo3s { get; set; }
	public DbSet<Foo4> Foo4s { get; set; }
	public DbSet<Foo5> Foo5s { get; set; }
	public DbSet<Foo6> Foo6s { get; set; }
	public DbSet<Foo7> Foo7s { get; set; }
	public DbSet<Foo8> Foo8s { get; set; }


	public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

	protected override void OnConfiguring(DbContextOptionsBuilder options)
	{
		Batteries.Init();
	}
}
