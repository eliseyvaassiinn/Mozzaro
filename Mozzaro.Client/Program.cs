using Mozzaro.Client.Components;
using Mozzaro.Client.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient<PizzaApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5088/");
});

builder.Services.AddScoped<HttpClient>(sp =>
{
    return new HttpClient
    {
        BaseAddress = new Uri("http://localhost:5088/")
    };
});

builder.Services.AddScoped<CartService>();
builder.Services.AddSingleton<UserSessionService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();