using Engrama.PWA;
using Engrama.PWA.Areas.LoginArea.Utiles;
using Engrama.PWA.Helpers;

using EngramaCoreStandar.Extensions;
using EngramaCoreStandar.Mapper;

using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

using MudBlazor.Services;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


var url = "https://engramaapi.azurewebsites.net/";
#if DEBUG
url = "https://localhost:7196/";
#endif
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(url) });
builder.Services.AddEngramaDependenciesBlazor();
builder.Services.AddMudServices();

builder.Services.AddAuthorizationCore();



builder.Services.AddSingleton<MapperHelper>(); // MapperHelper como singleton porque maneja su propia configuración


/*Engrama -> Services to call the API using the engrama Tools*/
builder.Services.AddScoped<LoadingState>();






builder.Services.AddScoped<EngramaAuthenticationProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, EngramaAuthenticationProvider>(
	provider => provider.GetRequiredService<EngramaAuthenticationProvider>()
	);
builder.Services.AddScoped<ILogginService, EngramaAuthenticationProvider>(
	provider => provider.GetRequiredService<EngramaAuthenticationProvider>()
	);

builder.Services.AddSingleton<UserSession>();

await builder.Build().RunAsync();
