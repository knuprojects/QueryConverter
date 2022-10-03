using QueryConverter.Core.Convension.Handlers;
using QueryConverter.Core.Convension.Processor;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IQueryHandler, QueryHandler>();
builder.Services.AddScoped<IQueryProcessor, QueryProcessor>();
builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
