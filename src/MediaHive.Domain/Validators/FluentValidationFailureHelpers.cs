using FluentValidation.Results;

namespace MediaHive.Domain.Validators;

public static class FluentValidationFailureHelpers
{
	public static Dictionary<string, string[]> DistinctErrorsByProperty(this ValidationResult? validationResult)
	{
		if (validationResult is null) return new Dictionary<string, string[]>();

		var errorsByProperty = DistinctErrorsByProperty(validationResult.Errors);
		return errorsByProperty;
	}

	public static Dictionary<string, string[]> DistinctErrorsByProperty(this IEnumerable<ValidationFailure>? validationFailures)
	{
		if (validationFailures is null) return new Dictionary<string, string[]>();

		var errorsByProperty = validationFailures
			.GroupBy(
				vf => vf.PropertyName,
				vf => vf.ErrorMessage,
				(pn, em) => new
				{
					Key = pn,
					Values = em.Distinct().ToArray()
				})
			.ToDictionary(
				pn => pn.Key,
				vf => vf.Values);

		return errorsByProperty;
	}
}