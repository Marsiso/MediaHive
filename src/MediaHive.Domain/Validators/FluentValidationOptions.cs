using FluentValidation;
using Microsoft.Extensions.Options;

namespace MediaHive.Domain.Validators;

public class FluentValidationOptions<TOptions> : IValidateOptions<TOptions> where TOptions : class
{
	private readonly IValidator<TOptions> _validator;

	public FluentValidationOptions(string name, IValidator<TOptions> validator)
	{
		_validator = validator;
		Name = name;
	}

	public string? Name { get; }

	public ValidateOptionsResult Validate(string? name, TOptions options)
	{
		if (name is not null && Name != name) return ValidateOptionsResult.Skip;

		ArgumentNullException.ThrowIfNull(options);

		var context = new ValidationContext<TOptions>(options);

		var validationResult = _validator.Validate(context);
		if (validationResult.IsValid) return ValidateOptionsResult.Success;

		var errorsByProperty = validationResult.DistinctErrorsByProperty();
		var errorMessage = errorsByProperty
			.Select(e => $"Failed property '{e.Key}' validation. Errors: '{string.Join(" ", e.Value)}'.")
			.Aggregate((l, r) => string.Join(" ", l, r));

		return ValidateOptionsResult.Fail(errorMessage);
	}
}