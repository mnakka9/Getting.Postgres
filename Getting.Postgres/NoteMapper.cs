using Getting.Postgres.Data;

using Riok.Mapperly.Abstractions;

namespace Getting.Postgres
{
	[Mapper]
	public static partial class NoteMapper
	{
		public static partial NoteDto MapNoteToNoteDto (Note note);
		public static partial Note MapNoteDtoToNote (NoteDto note);
	}
}
