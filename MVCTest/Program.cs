using MVCTest.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<ICountryInfoService, CountryInfoService>();
builder.Services.AddScoped<INewsService, NewsService>();
builder.Services.AddScoped<IWeatherService, WeatherService>();
builder.Services.AddScoped<ICurrencyService, CurrencyService>();
builder.Services.AddSingleton<ICountryService, CountryService>();
builder.Services.AddHttpClient();
var app = builder.Build();

var countryService = app.Services.GetRequiredService<ICountryService>();

Task.Run(async () =>
{
    await countryService.LoadCountriesAsync();
    Console.WriteLine("Countries loaded.");
});


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
