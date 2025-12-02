using Microsoft.EntityFrameworkCore;
using VivesBlog.Api.Installers;
using VivesBlog.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder
    .InstallRestApi()
    .InstallSwagger()
    .InstallDatabase()
    .InstallServices()
    .InstallAuthentication()
    .InstallIdentity();

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

app.UseCors("VivesBlogCorsPolicy");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
