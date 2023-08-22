using MediaHive.Domain.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace MediaHive.Data.EF;

public class MediaHiveContext : DbContext
{
	private readonly IOptions<MediaHiveContextOptions> _sqliteOptions;
	
	public MediaHiveContext(IOptions<MediaHiveContextOptions> sqliteOptions)
	{
		_sqliteOptions = sqliteOptions;
	}

	public MediaHiveContext(DbContextOptions<MediaHiveContext> options, IOptions<MediaHiveContextOptions> sqliteSqliteOptions) : base(options)
	{
		_sqliteOptions = sqliteSqliteOptions;
	}

	public DbSet<User> Users { get; set; } = default!;
	
	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		base.OnConfiguring(optionsBuilder);

		var dataSource = Path.Combine(_sqliteOptions.Value.Path, _sqliteOptions.Value.FileName);
		var connectionStringBase = $"Data Source={dataSource};";

		var connectionString = new SqliteConnectionStringBuilder(connectionStringBase)
		{
			Mode = SqliteOpenMode.ReadWriteCreate
		}.ToString();

		optionsBuilder.UseSqlite(connectionString);
		
		optionsBuilder.EnableDetailedErrors();
		optionsBuilder.EnableSensitiveDataLogging();
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediaHiveContext).Assembly);
	}
}