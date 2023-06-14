namespace KafkaTemplate.Core.Entities
{
    public class MetaData
    {
        public DateTime CreatedDateTime { get; set; }
        public string? CreatedBy { get; set; }
        public DateTime ModifiedDateTime { get; set; }
        public string? ModifiedBy { get; set; }
        public bool IsActive { get; set; }
    }
}
