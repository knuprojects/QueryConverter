using QueryConverter.Presentation.Infrastructure;
using QueryConverter.Shared.Cqrs;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCore();
builder.Services.AddCqrs(Assembly.GetExecutingAssembly());
builder.Services.AddDefaultServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCore();

app.UseCors("AllowAll");

app.MapControllers();

app.Run();
