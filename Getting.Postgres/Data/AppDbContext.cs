// Ignore Spelling: Postgres

using Microsoft.EntityFrameworkCore;

namespace Getting.Postgres.Data
{
	public class AppDbContext : DbContext
	{
		public AppDbContext (DbContextOptions<AppDbContext> options) : base(options) { }

		public DbSet<Note> Notes { get; set; }
	}
}
