using FluentValidation;

namespace MediaHive.Data.EF;

public class MediaHiveContextOptionsValidator : AbstractValidator<MediaHiveContextOptions>
{
	public MediaHiveContextOptionsValidator()
	{
		RuleFor(o => o.Path)
			.NotEmpty()
			.WithMessage("SQLite database file path is required.")
			.Must(Directory.Exists)
			.WithMessage(o => $"SQLite database file directory doesn't exist. Directory path: '{o.Path}'.");

		RuleFor(o => o.FileName)
			.NotEmpty()
			.WithMessage("SQLite database filename is required.")
			.Must(Path.HasExtension)
			.WithMessage(o => $"SQLite database filename doesn't have extension. Filename: '{o.FileName}'.")
			.Matches(@"^.+\.db$")
			.WithMessage(o => $"SQLite database filename has invalid format. Filename: '{o.FileName}'.");
	}
}