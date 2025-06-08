using Microsoft.EntityFrameworkCore;
using Presentation.Data;
using Presentation.Data.Repositories;
using Presentation.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IEventRespository, EventRespository>();
builder.Services.AddDbContext<DataContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("EventDatabaseConnection")));

var app = builder.Build();
app.MapOpenApi();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Service API");
    c.RoutePrefix = string.Empty;
});
app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
