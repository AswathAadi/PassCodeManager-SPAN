namespace PassCodeManager.Classified.Entities
{
    public class TblLogs
    {
        public string Id { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public DateTime? EventTime { get; set; }
        public string Event { get; set; }
    }
}
