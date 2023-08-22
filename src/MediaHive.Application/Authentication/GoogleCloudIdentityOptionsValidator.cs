using FluentValidation;

namespace MediaHive.Application.Authentication;

public class GoogleCloudIdentityOptionsValidator : AbstractValidator<GoogleCloudIdentityOptions>
{
	public GoogleCloudIdentityOptionsValidator()
	{
		RuleFor(o => o.ClientID)
			.NotEmpty();
		
		RuleFor(o => o.ClientSecret)
			.NotEmpty();
		
		RuleFor(o => o.CallbackPath)
			.NotEmpty();
	}
}