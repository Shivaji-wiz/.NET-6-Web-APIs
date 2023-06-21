using System.ComponentModel.DataAnnotations;

namespace CorpEstate.DAL.DTO
{
    public class UserDTO
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string RoleName { get; set; }
        [Required]
        public string Password { get; set; }
        //[Required]
        //public DateTime DOB { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
