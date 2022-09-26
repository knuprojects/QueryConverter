using QueryConverter.Core.Convension.Handlers;
using QueryConverter.Core.Convension.Processor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IQueryHandler, QueryHandler>();
builder.Services.AddScoped<IQueryProcessor, QueryProcessor>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
