using as_sensors_application.Observability;
using Prometheus;

namespace as_sensors_api.Observability;

public class PrometheusSensorTelemetry : ISensorTelemetry
{
    private const string Service = "as.sensors.api";
    private const string Env = "dev";

    private static readonly Counter SensorsCreatedTotal = 
        Metrics.CreateCounter("as_sensors_created_total", "Sensores criados",
            new CounterConfiguration { LabelNames = ["service", "env", "field_id"] });

    private static readonly Counter SensorReadingsStoredTotal =
        Metrics.CreateCounter("as_sensors_readings_stored_total", "Leituras armazenadas",
            new CounterConfiguration { LabelNames = ["service", "env", "field_id", "sensor_id"] });

    private static readonly Gauge TemperatureC =
        Metrics.CreateGauge("as_sensors_temeprature_c", "Temperatura atual em (C)",
            new GaugeConfiguration { LabelNames = ["service", "env", "field_id", "sensor_id"] });

    private static readonly Gauge SoilMoisturePercentage =
        Metrics.CreateGauge("as_sensors_soil_moisture_percentage", "Umidade do solo (%)",
            new GaugeConfiguration { LabelNames = ["service", "env", "field_id", "sensor_id"] });

    private static readonly Gauge PrecipitationPercentage =
        Metrics.CreateGauge("as_sensors_precipitation_percentage", "Nível de precipitação (%)",
            new GaugeConfiguration { LabelNames = ["service", "env", "field_id", "sensor_id"] });

    private static readonly Counter FieldStatusUpdatePublishTotal =
        Metrics.CreateCounter("as_sensors_field_status_update_publish_total", "Atualizações de status de campo publicadas",
            new CounterConfiguration { LabelNames = ["service", "env", "field_id", "status", "result", "failure_reason"] });

    // Status atual do talhão
    private static readonly Gauge FieldStatusCurrent =
    Metrics.CreateGauge(
        "as_sensors_field_status_current",
        "Status atual do talhao",
        new GaugeConfiguration
        {
            LabelNames = ["field_id"]
        });

    public void FieldStatusUpdatePublished(Guid fieldId, string status, bool success, string? failureReaon = null)
    {
        FieldStatusUpdatePublishTotal
            .WithLabels(Service, Env, status, success ? "success" : "failure", failureReaon ?? "")
            .Inc();
    }

    public void SensorCreated(Guid fieldId)
    {
        SensorsCreatedTotal.WithLabels(Service, Env, fieldId.ToString()).Inc();
    }

    public void SensorReadingStored(Guid fieldId, Guid sensorId, double temperatureC, double soilMoisturePercentage, double preciptationPercentage)
    {
        SensorReadingsStoredTotal.WithLabels(Service, Env, fieldId.ToString(), sensorId.ToString()).Inc();
        TemperatureC.WithLabels(Service, Env, fieldId.ToString(), sensorId.ToString()).Set(temperatureC);
        SoilMoisturePercentage.WithLabels(Service, Env, fieldId.ToString(), sensorId.ToString()).Set(soilMoisturePercentage);
        PrecipitationPercentage.WithLabels(Service, Env, fieldId.ToString(), sensorId.ToString()).Set(preciptationPercentage);
    }

    public void StatusAlert(Guid fieldId, string status, string? failureReaon = null)
    {
        double numericStatus = status switch
        {
            "Unknown" => 0,
            "Normal" => 1,
            "DroughtAlert" => 2,
            "PestRisk" => 3,
            "FloodRisk" => 4,
            "FrostWarning" => 5,
            _ => 0
        };

        FieldStatusCurrent
        .WithLabels(fieldId.ToString())
        .Set(numericStatus);
    }

}
