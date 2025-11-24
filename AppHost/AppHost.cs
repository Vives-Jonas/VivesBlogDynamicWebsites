using Projects;

var builder = DistributedApplication.CreateBuilder(args);

builder.AddProject<VivesBlog_Api>("API");
builder.AddProject<VivesBlog_Blazor>("Blazor");
builder.AddProject<VivesBlog_Mvc>("MVC");

builder.Build().Run();
