using Ecommerce.Components;
using Ecommerce.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient(); // This is enough to add HttpClient globally

// Register ProductService, which will automatically get HttpClient injected
builder.Services.AddSingleton<ProductService>();

builder.Services.AddHttpClient<CartService>(client => 
{
    client.BaseAddress = new Uri("https://prof-elec.vercel.app/");
});
builder.Services.AddSingleton<CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
