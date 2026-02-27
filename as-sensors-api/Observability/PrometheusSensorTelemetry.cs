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
        Metrics.CreateGauge("as_sensors_temperature_c", "Temperatura atual em (C)",
            new GaugeConfiguration { LabelNames = ["service", "env", "field_id", "sensor_id"] });

    private static readonly Gauge SoilMoisturePercentage =
        Metrics.CreateGauge("as_sensors_soil_moisture_percentage", "Umidade do solo (%)",
            new GaugeConfiguration { LabelNames = ["service", "env", "field_id", "sensor_id"] });

    private static readonly Gauge PrecipitationPercentage =
        Metrics.CreateGauge("as_sensors_precipitation_percentage", "Nível de precipitação (%)",
            new GaugeConfiguration { LabelNames = ["service", "env", "field_id", "sensor_id"] });

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

}
