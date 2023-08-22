using MediaHive.Application.Security;
using MediaHive.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddGoogleCloudIdentity(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseHsts();
}

var policies = SecurityHeaderHelpers.GetHeaderPolicyCollection(app.Environment.IsDevelopment());
app.UseSecurityHeaders(policies);

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCookiePolicy();

app.UseAuthentication();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();