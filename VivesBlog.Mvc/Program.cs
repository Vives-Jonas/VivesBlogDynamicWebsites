using VivesBlog.Mvc.Settings;
using VivesBlog.Sdk;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


var appSettings = new AppSettings();
builder.Configuration.Bind(nameof(AppSettings), appSettings);
builder.Services.AddSingleton(appSettings);

builder.Services.AddHttpClient("VivesBlogApi", (provider, client) =>
{
    client.BaseAddress = new Uri(appSettings.ApiBaseUrl);
});


builder.Services.AddScoped<BlogSdkService>();
builder.Services.AddScoped<PersonSdkService>();

////nieuwe service registreren:
//builder.Services.AddDbContext<VivesBlogDbContext>(options =>
//{
//    //voeg unieke naam toe!
//    options.UseInMemoryDatabase(nameof(VivesBlogDbContext));
//});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    //using var scope = app.Services.CreateScope();

    //var dbContext = scope.ServiceProvider.GetRequiredService<VivesBlogDbContext>();
    //if (dbContext.Database.IsInMemory())
    //{
    //    await dbContext.Seed();
    //}
    

    
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
