using Firewall;
using MonikaChat.Server.Formatters;
using MonikaChat.Server.Interfaces;
using MonikaChat.Server.Models.Cryptography;
using MonikaChat.Server.Models.OpenAI;
using MonikaChat.Server.Services;
using System.Net;
using System.Net.Sockets;

var builder = WebApplication.CreateBuilder(args);

// Add JSON file configuration (default behavior)
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
					 .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);

// Add environment variables
builder.Configuration.AddEnvironmentVariables();

// Configure CORS policy
string corsDomain = builder.Configuration["CORS_POLICY_DOMAIN"] ?? string.Empty;

builder.Services.Configure<OpenAIOptions>(
	builder.Configuration.GetSection(OpenAIOptions.Name));
builder.Services.Configure<CryptographyOptions>(
	builder.Configuration.GetSection(CryptographyOptions.Name));

// Add services to the container.

builder.Services.AddControllers(o => o.InputFormatters.Insert(o.InputFormatters.Count, new TextPlainInputFormatter()));
builder.Services.AddScoped<ILLMService, OpenAIService>();
builder.Services.AddHttpClient<ILLMService, OpenAIService>();
builder.Services.AddScoped<CryptographyService>();
builder.Services.AddScoped<MonikaService>();
builder.Services.AddHealthChecks();

// Making it run in headless mode while not in Development environment
if (builder.Environment.IsDevelopment())
{
	builder.Services.AddControllersWithViews();
	builder.Services.AddRazorPages();
}
else
{
	builder.Services.AddControllers();
}

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseWebAssemblyDebugging();
}

app.MapHealthChecks("/healthz");

app.UseHttpsRedirection();

// Making it run in headless mode while not in Development environment
if (app.Environment.IsDevelopment())
{
	app.UseBlazorFrameworkFiles();
	app.UseStaticFiles();

	app.MapRazorPages();
	app.MapFallbackToFile("index.html");
}

app.MapControllers();

app.UseAuthorization();
//if (!string.IsNullOrWhiteSpace(corsDomain))
//{
//	app.UseCors(policy =>
//		policy.WithOrigins(corsDomain)
//			  .AllowAnyMethod()
//			  .AllowAnyHeader()
//			  .AllowCredentials());
//}

// DigitalOcean does not have a Firewall feature on App Platform
// So I'm bringing my own
var rules =
	FirewallRulesEngine
		.DenyAllAccess()
		.ExceptFromCloudflare()
		.ExceptWhen(ctx => Dns.GetHostAddresses(Dns.GetHostName())
			.Where(ip => ip.AddressFamily == AddressFamily.InterNetwork || ip.AddressFamily == AddressFamily.InterNetworkV6)
			.Select(ip => ip.ToString())
			.Contains(ctx.Connection.RemoteIpAddress?.ToString()))
		.ExceptFromLocalhost();

app.UseFirewall(rules);

app.Run();
