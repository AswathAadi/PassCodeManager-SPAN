namespace PassCodeManager.Classified.Entities
{
    [Serializable]
    public class TblPassCodes
    {
        public TblPassCodes()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string Name { get; set; }
        public string PassCode { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
        public DateTime UpdatedOn { get; set; }
        public virtual TblSecurityKeys SecurityKeys { get; set; }
    }
}
