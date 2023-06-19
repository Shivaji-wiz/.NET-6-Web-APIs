using CorpEstate.BLL.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorpEstate.DAL.DTO
{
    public class PropertyReviewDTO
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Property")]
        public int Property_Id { get; set; }
        public virtual Property Property { get; set; }
    }
}
