using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace MediaHive.Application.ViewModels.Identity;

public class LogoutViewModel : PageModel
{
	public LogoutViewModel(ILogger<LogoutViewModel> logger)
	{
		_logger = logger;
	}

	private ILogger<LogoutViewModel> _logger;

	public string? ReturnUrl { get; private set; }
	
	public async Task<IActionResult> OnGetAsync(string? returnUrl = default)
	{
		returnUrl = returnUrl ?? Url.Content($"~{Routes.Index}");
		
		try
		{
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
		}
		catch (Exception exception)
		{
			var message = $"Authentication operation failure. View model: '{nameof(LogoutViewModel)}' Action: '{nameof(OnGetAsync)}' Error: '{nameof(exception.Message)}'.";
			
			_logger.LogError(message);
		}
		
		return LocalRedirect(Routes.Index);
	}
}