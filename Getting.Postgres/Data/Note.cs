using System.ComponentModel.DataAnnotations;

namespace Getting.Postgres.Data
{
	public class Note
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public string? Title { get; set; }
		[Required]
		public string? Content { get; set; }

		public DateTime CreatedDate = DateTime.UtcNow;
	}
}
