namespace MediaHive.Data.EF;

public class MediaHiveContextOptions
{
	public const string SectionName = "SQLite";

	public required string Path { get; set; }
	public required string FileName { get; set; }
	public required string Password { get; set; }
}