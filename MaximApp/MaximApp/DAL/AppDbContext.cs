

namespace MaximApp.DAL
{
	public class AppDbContext : IdentityDbContext<User>
	{
		public AppDbContext(DbContextOptions options) : base(options)
		{
		}
		public DbSet<Service> Services { get; set; }
		public DbSet<User> Users {  get; set; }

	}
}
