using QueryConverter.Core.Handlers;
using QueryConverter.Core.Handlers.Commands;
using QueryConverter.Presentation.Infrastructure;
using QueryConverter.Shared.Cqrs.Commands;
using QueryConverter.Shared.Cqrs.Queries;
using QueryConverter.Types.Shared.Dto;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCqrs();
builder.Services.AddCore();
builder.Services.AddDefaultServices();

builder.Services.AddTransient<ICommand<ResultModel>, SelectCommand>();
builder.Services.AddTransient<ICommand<ResultModel>, OrderByCommand>();
builder.Services.AddTransient<ICommand<ResultModel>, GroupByCommand>();

builder.Services.AddTransient<ICommandHandler<SelectCommand, ResultModel>, SelectCommandHandler>();
builder.Services.AddTransient<ICommandHandler<OrderByCommand, ResultModel>, OrderByCommandHandler>();
builder.Services.AddTransient<ICommandHandler<GroupByCommand, ResultModel>, GroupByCommandHandler>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCore();

app.MapControllers();

app.Run();
