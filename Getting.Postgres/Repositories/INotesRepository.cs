using Getting.Postgres.Data;

namespace Getting.Postgres.Repositories
{
	public interface INotesRepository : IRepository<Note>
	{
	}

	public class UnitOfWork : IUnitOfWork, IDisposable
	{
		private readonly AppDbContext _appDbContext;
		public INotesRepository NotesRepository { get; private set; }
		private bool disposed = false;
		public UnitOfWork (AppDbContext appDbContext, INotesRepository notesRepository)
		{
			_appDbContext = appDbContext;
			NotesRepository = notesRepository;
		}

		public async Task<int> CommitAsync ()
		{
			return await _appDbContext.SaveChangesAsync ();
		}

		public void Dispose ()
		{
			if(disposed) return;

			Dispose(true);

			GC.SuppressFinalize(this);
		}

		public void Dispose(bool isDisposing)
		{
			if (isDisposing)
			{
				_appDbContext.Dispose();
				disposed = true;
			}
		}

		public void DisposeContext ()
		{
			Dispose();
		}
	}

	public interface IUnitOfWork
	{
		INotesRepository NotesRepository { get; }
		void DisposeContext ();
		Task<int> CommitAsync ();
	}
}
