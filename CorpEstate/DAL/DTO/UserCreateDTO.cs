using System.ComponentModel.DataAnnotations;

namespace CorpEstate.DAL.DTO
{
    public class UserCreateDTO
    {
        //[Key]
        //[Required]
        //public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string Password { get; set; } = string.Empty;

        public DateTime CreatedTime { get; set; } = DateTime.Now;
    }
}
