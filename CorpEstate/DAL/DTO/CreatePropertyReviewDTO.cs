using System.ComponentModel.DataAnnotations;

namespace CorpEstate.DAL.DTO
{
    public class CreatePropertyReviewDTO
    {
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        public int Property_Id { get; set; }
    }
}
