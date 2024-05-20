using System.ComponentModel.DataAnnotations;

namespace PassCodeManager.Classified.Entities
{
    [Serializable]
    public partial class TblUsers
    {
        public TblUsers()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime RegistrationDate { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsLockedOut { get; set; }
        public bool IsActive { get; set; }
        public string Mobile { get; set; }
        public int VerifiedMediums { get; set; }
    }
}
