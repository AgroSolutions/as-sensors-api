namespace as_sensors_application.Observability;

public interface ISensorTelemetry
{
    void SensorCreated(Guid fieldId);
    void SensorReadingStored(Guid fieldId, Guid sensorId, double temperatureC, double soilMoisturePercentage, double preciptationPercentage);
    void FieldStatusUpdatePublished(Guid fieldId, string status, bool success, string? failureReaon = null);
    void StatusAlert(Guid fieldId, string status, string? failureReaon = null);

}
