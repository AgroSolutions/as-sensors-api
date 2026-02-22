using as_sensors_infra.Persistance.Config;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DI
builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<as_sensors_application.Services.SensorDataService>();
builder.Services.AddScoped<as_sensors_infra.Persistance.Repository.Interfaces.ISensorDataRepository,
    as_sensors_infra.Persistance.Repository.SensorDataRepository>();
builder.Services.AddScoped<as_sensors_application.Services.SensorService>();
builder.Services.AddScoped<as_sensors_infra.Persistance.Repository.Interfaces.ISensorRepository,
    as_sensors_infra.Persistance.Repository.SensorRepository>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
