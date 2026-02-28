using as_sensors_api;
using as_sensors_api.Configurations;
using as_sensors_api.Observability;
using as_sensors_application.DTO;
using as_sensors_application.Handler;
using as_sensors_application.Observability;
using as_sensors_application.Publishers;
using as_sensors_application.Publishers.Interfaces;
using as_sensors_application.Services;
using as_sensors_domain.Messaging.Interfaces;
using as_sensors_infra;
using as_sensors_infra.Messaging.Config;
using as_sensors_infra.Persistance.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using Prometheus;
using System.Text;

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

builder.Services.Configure<MongoDBSettings>(
    builder.Configuration.GetSection("MongoDBSettings"));
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
builder.Services.AddSingleton<ISensorTelemetry, PrometheusSensorTelemetry>();

#region JWT
// ✅ JWT Authentication
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var chaveSecreta = jwtSettings["Key"];

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(chaveSecreta!)),
        ClockSkew = TimeSpan.Zero
    };
});
#endregion

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
builder.Services.AddHostedService<WorkerCreateSensor>();

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

app.UseRouting();
app.UseHttpMetrics();
app.MapMetrics();

app.UseAuthorization();

app.MapControllers();

app.Run();
