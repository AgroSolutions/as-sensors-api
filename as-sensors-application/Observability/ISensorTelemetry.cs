namespace as_sensors_application.Observability;

public interface ISensorTelemetry
{
    void SensorCreated(Guid filedId);
    void SensorReadingStored(Guid sensorId, double temperatureC, double soilMoisturePercentage, double preciptationPercentage);
    void FieldStatusUpdatePublished(string status, bool success, string? failureReaon = null);
    void StatusAlert(Guid fieldId, string status, string? failureReaon = null);

}
