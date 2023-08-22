using MediaHive.Application.Security;
using MediaHive.Data.EF;
using MediaHive.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddDbConnection(builder.Configuration);
builder.Services.AddGoogleCloudIdentity(builder.Configuration);

var app = builder.Build();

using var scope = app.Services.CreateScope();
var context = scope.ServiceProvider.GetRequiredService<MediaHiveContext>();

if (!app.Environment.IsDevelopment())
{
	context.Database.EnsureDeleted();
	context.Database.EnsureCreated();
}
else
{
	context.Database.EnsureCreated();
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