using System.ComponentModel.DataAnnotations;

namespace CorpEstate.BLL.Model
{
    public class User
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        //[Required]
        //public string Role { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
