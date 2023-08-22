using FluentValidation;
using MediaHive.Application.Authentication;
using MediaHive.Data.EF;
using MediaHive.Domain.Validators;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace MediaHive.Web;

public static class ConfigurationExtensions
{
	public static IServiceCollection AddDbConnection(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddSingleton<IValidator<MediaHiveContextOptions>, MediaHiveContextOptionsValidator>();
		
		services.AddOptions<MediaHiveContextOptions>()
			.Bind(configuration.GetSection(MediaHiveContextOptions.SectionName))
			.ValidateFluently()
			.ValidateOnStart();

		services.AddDbContext<MediaHiveContext>();

		return services;
	}
	
	public static IServiceCollection AddGoogleCloudIdentity(this IServiceCollection services, IConfiguration configuration)
	{
		var googleCloudIdentityConfigurationSection = configuration.GetSection(GoogleCloudIdentityOptions.SegmentName);
		
		ArgumentNullException.ThrowIfNull(googleCloudIdentityConfigurationSection);

		var googleCloudIdentityOptions = googleCloudIdentityConfigurationSection.Get<GoogleCloudIdentityOptions>();
		
		ArgumentNullException.ThrowIfNull(googleCloudIdentityOptions);

		services.AddSingleton<IValidator<GoogleCloudIdentityOptions>, GoogleCloudIdentityOptionsValidator>();
		
		services.AddOptions<GoogleCloudIdentityOptions>()
			.Bind(googleCloudIdentityConfigurationSection)
			.ValidateFluently()
			.ValidateOnStart();
		
		services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			.AddCookie();

		services.AddAuthentication()
			.AddGoogle(options =>
			{
				options.ClientId = googleCloudIdentityOptions.ClientID;
				options.ClientSecret = googleCloudIdentityOptions.ClientSecret;
				options.CallbackPath = googleCloudIdentityOptions.CallbackPath;
				options.ClaimActions.MapJsonKey("urn:google:profile", "link");
				options.ClaimActions.MapJsonKey("urn:google:image", "picture");
			});

		services.AddHttpContextAccessor();
		services.AddScoped<HttpContextAccessor>();

		services.AddHttpClient();
		services.AddScoped<HttpClient>();
		
		return services;
	}
}