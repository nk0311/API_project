using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using MyFirstAPI.Models;
namespace MyFirstAPI.Models
{
	public class MyFirstAPIDBContext : DbContext
	{
		protected readonly IConfiguration Configuration;
		public MyFirstAPIDBContext(DbContextOptions<MyFirstAPIDBContext> options, IConfiguration configuration)
			: base(options)
		{
			Configuration = configuration;
		}

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			var connectionString = Configuration.GetConnectionString("CarInventoryService");
			options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
		}

		public DbSet<User> Users { get; set; } = null!;
		public DbSet<Car> Cars { get; set; } = null!;
	}
}

