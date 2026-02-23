using as_sensors_application.Observability;
using Prometheus;
using System.Runtime.CompilerServices;

namespace as_sensors_api.Observability;

public class PrometheusSensorTelemetry : ISensorTelemetry
{
    private const string Service = "as.sensors.api";
    private const string Env = "dev";

    private static readonly Counter SensorsCreatedTotal = 
        Metrics.CreateCounter("as_sensors_created_total", "Sensores criados",
            new CounterConfiguration { LabelNames = new[] { "service", "env" } });

    private static readonly Counter SensorReadingsStoredTotal =
        Metrics.CreateCounter("as_sensors_readings_stored_total", "Leituras armazenadas",
            new CounterConfiguration { LabelNames = new[] { "service", "env" } });

    private static readonly Gauge TemperatureC =
        Metrics.CreateGauge("as_sensors_temeprature_c", "Temperatura atual em (C)",
            new GaugeConfiguration { LabelNames = new[] { "service", "env" } });

    private static readonly Gauge SoilMoisturePercentage =
        Metrics.CreateGauge("as_sensors_soil_moisture_percentage", "Umidade do solo (%)",
            new GaugeConfiguration { LabelNames = new[] { "service", "env" } });

    private static readonly Gauge PrecipitationPercentage =
        Metrics.CreateGauge("as_sensors_precipitation_percentage", "Nível de precipitação (%)",
            new GaugeConfiguration { LabelNames = new[] { "service", "env" } });

    private static readonly Counter FieldStatusUpdatePublishTotal =
        Metrics.CreateCounter("as_sensors_field_status_update_publish_total", "Atualizações de status de campo publicadas",
            new CounterConfiguration { LabelNames = new[] { "service", "env", "status", "result", "failure_reason" } });
    public void FieldStatusUpdatePublished(string status, bool success, string? failureReaon = null)
    {
        FieldStatusUpdatePublishTotal
            .WithLabels(Service, Env, status, success ? "success" : "failure", failureReaon ?? "")
            .Inc();
    }

    public void SensorCreated(Guid filedId)
    {
        SensorsCreatedTotal.WithLabels(Service, Env).Inc();
    }

    public void SensorReadingStored(Guid sensorId, double temperatureC, double soilMoisturePercentage, double preciptationPercentage)
    {
        SensorReadingsStoredTotal.WithLabels(Service, Env).Inc();
        TemperatureC.WithLabels(Service, Env).Set(temperatureC);
        SoilMoisturePercentage.WithLabels(Service, Env).Set(soilMoisturePercentage);
        PrecipitationPercentage.WithLabels(Service, Env).Set(preciptationPercentage);
    }
}
