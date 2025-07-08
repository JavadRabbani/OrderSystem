using API.Extensions;
using Application.StartupExtensions;
using Infrastructure.StartupExtentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplication()
    .AddInfrastructure()
    .AddApiServices().ConfigureMapster();

var app = builder.Build();

app.UseCustomMiddlewares();

app.MapControllers();
app.Run();

public partial class Program
{ }