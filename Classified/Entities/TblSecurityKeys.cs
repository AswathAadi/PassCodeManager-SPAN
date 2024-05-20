﻿namespace PassCodeManager.Classified.Entities
{
    [Serializable]
    public class TblSecurityKeys
    {
        public TblSecurityKeys()
        {
            Id = Guid.NewGuid().ToString();
        }
        public string Id { get; set; }
        public string PublicKey { get; set; }

        public string PrivateKey { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Mobile { get; set; }
        public bool IsActive { get; set; }
        public string PassCodeId { get; set; }
        public virtual ICollection<TblPassCodes> HasPassCodes { get; set; } = new HashSet<TblPassCodes>();
    }
}
