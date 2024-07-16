using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MonikaChat.Client;
using MonikaChat.Client.Interfaces;
using MonikaChat.Client.Services;
using TG.Blazor.IndexedDB;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddIndexedDB(dbConfig =>
{
	dbConfig.DbName = IndexedDbLongTermAIMemoryService.DbName;
	dbConfig.Version = IndexedDbLongTermAIMemoryService.DbVersion;

	dbConfig.Stores.Add(IndexedDbLongTermAIMemoryService.StoreSchema);
});

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


string baseAddress = builder.HostEnvironment.BaseAddress;

// Haven't figured out a better way how to do this yet :<
if (baseAddress.Contains("monika.chat"))
{
	baseAddress = "https://api.monika.chat/";
}

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(baseAddress) });
builder.Services.AddScoped<CryptographyService>();
builder.Services.AddScoped<ISettingsService, LocalStorageSettingsService>();
builder.Services.AddScoped<IAIInteractionService, MonikaInteractionService>();
builder.Services.AddScoped<ILongTermMemoryService, IndexedDbLongTermAIMemoryService>();

await builder.Build().RunAsync();
