using MonikaChat.Server.Formatters;
using MonikaChat.Server.Interfaces;
using MonikaChat.Server.Models.Cryptography;
using MonikaChat.Server.Models.OpenAI;
using MonikaChat.Server.Services;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();
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

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();

app.UseAuthorization();

app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
