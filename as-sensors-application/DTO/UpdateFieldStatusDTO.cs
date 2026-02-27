using as_sensors_domain.Enum;

namespace as_sensors_application.DTO
{
    public class UpdateFieldStatusDTO
    {
        public required Guid FieldId {  get; set; }
        public required FieldStatus Status { get; set; }
        public required DateTime UpdatedAt { get; set; }
    }
}
