using Microsoft.EntityFrameworkCore;
using VivesBlog.Repository;
using VivesBlog.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//Add Swagger
builder.Services.AddSwaggerGen();


//var connectionString = builder.Configuration.GetConnectionString(nameof(PeopleManagerDbContext));

builder.Services.AddDbContext<VivesBlogDbContext>(options =>
{
    options.UseInMemoryDatabase(nameof(VivesBlogDbContext));
    //options.UseSqlServer(connectionString);
});



builder.Services.AddScoped<BlogService>();
builder.Services.AddScoped<PersonService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
    using var scope = app.Services.CreateScope();

    var dbContext = scope.ServiceProvider.GetRequiredService<VivesBlogDbContext>();
    if (dbContext.Database.IsInMemory())
    {
        await dbContext.Seed();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
