using QueryConverter.Presentation.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCqrs();
builder.Services.AddCore();
builder.Services.AddDefaultServices();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCore();

app.MapControllers();

app.Run();
