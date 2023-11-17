using Getting.Postgres.Data;

namespace Getting.Postgres.Repositories
{
	public class NotesRepository : Repository<Note>, INotesRepository
	{
		public NotesRepository (AppDbContext dbContext) : base(dbContext)
		{
		}
	}
}
