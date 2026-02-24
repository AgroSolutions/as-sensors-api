using as_sensors_infra.Persistance.Config;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Bson.Serialization;
using MongoDB.Bson;
using as_sensors_infra.Messaging.Config;
using as_sensors_infra;
using as_sensors_api.Configurations;
using as_sensors_domain.Messaging.Interfaces;
using as_sensors_application.DTO;
using as_sensors_application.Handler;
using as_sensors_api;
using as_sensors_application.Services;
using as_sensors_application.Publishers.Interfaces;
using as_sensors_application.Publishers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

#region Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerConfiguration();
#endregion

#region DI
builder.Services.AddHttpClient();

builder.Services.AddSingleton<MongoContext>();
builder.Services.AddScoped<FieldService>();
builder.Services.AddScoped<IFieldServicePublisher, FieldServicePublisher>();
builder.Services.AddScoped<as_sensors_application.Services.SensorDataService>();
builder.Services.AddScoped<as_sensors_infra.Persistance.Repository.Interfaces.ISensorDataRepository,
    as_sensors_infra.Persistance.Repository.SensorDataRepository>();
builder.Services.AddScoped<as_sensors_application.Services.Interfaces.ISensorService,
    as_sensors_application.Services.SensorService>();
//builder.Services.AddScoped<as_sensors_application.Services.SensorService>();
builder.Services.AddScoped<as_sensors_infra.Persistance.Repository.Interfaces.ISensorRepository,
    as_sensors_infra.Persistance.Repository.SensorRepository>();

#region Messaging


//services.AddTransient<IMessageHandler<MessageDTO>, GameIncreasePopularityHandler>();

builder.Services.AddTransient<IMessageHandler<SensorDTOResquest>, CreateSensorHandler>();

var messagingSection = builder.Configuration.GetSection("Messaging");
if (!messagingSection.Exists())
    throw new InvalidOperationException("Section 'Messaging' not found in configuration.");

var queuesSection = messagingSection.GetSection("Queues");
builder.Services.Configure<QueuesOptions>(queuesSection);

builder.Services.ConfigureAmazonSQS(builder.Configuration);
#endregion

#endregion

// ✅ Worker
//builder.Services.AddHostedService<WorkerCreateSensor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
//app.UseSwagger();
//app.UseSwaggerUI();

if (app.Environment.IsDevelopment() || app.Environment.IsStaging() || app.Environment.IsProduction())
{
    app.UseSwaggerConfiguration();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseDeveloperExceptionPage();
app.UseExceptionHandler("/Error");
app.UseHsts();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
