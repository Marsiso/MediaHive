using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MediaHive.Application.ViewModels.Identity;

[AllowAnonymous]
public class LoginViewModel : PageModel
{
	public LoginViewModel(ILogger<LoginViewModel> logger)
	{
		_logger = logger;
	}

	private ILogger<LoginViewModel> _logger;

	[HttpGet]
	public IActionResult OnGet(string? returnUrl = default)
	{
		const string provider = "Google";
		
		var authenticationProperties = new AuthenticationProperties
		{
			RedirectUri = Url.Page("./Login", pageHandler: "Callback", values: new { returnUrl }),
		};
		
		return new ChallengeResult(provider, authenticationProperties);
	}
	
	[HttpGet]
	public async Task<IActionResult> OnGetCallbackAsync(string? returnUrl = null, string? remoteError = null)
	{
		var GoogleUser = User.Identities.FirstOrDefault();

		var authenticated = GoogleUser?.IsAuthenticated ?? false;

		if (!authenticated) return LocalRedirect(Routes.Index);
		
		var authProperties = new AuthenticationProperties
		{
			IsPersistent = true,
			RedirectUri = Request.Host.Value
		};
		
		var principal = new ClaimsPrincipal(GoogleUser!);
		
		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);
		
		return LocalRedirect(Routes.Index);
	}
}