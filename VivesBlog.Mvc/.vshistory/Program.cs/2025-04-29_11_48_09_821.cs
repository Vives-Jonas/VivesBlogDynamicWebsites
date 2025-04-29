using Microsoft.EntityFrameworkCore;
using VivesBlog.Repository;
using VivesBlog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<BlogService>();
//nieuwe service registreren:
builder.Services.AddDbContext<BlogPostDbContext>(options =>
{
    //voeg unieke naam toe!
    options.UseInMemoryDatabase(nameof(BlogPostDbContext));
});

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
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<BlogPostDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        dbContext.Seed();
    }
    

    
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
