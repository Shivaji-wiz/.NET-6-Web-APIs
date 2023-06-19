using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorpEstate.BLL.Model
{
    public class PropertyReview
    {
        [Key]
        public int ReviewId { get; set; }
        [Required]
        public int Rating { get; set; }
        public string Comment { get; set; }
        [Required]
        [ForeignKey("Property")]
        public int Property_Id { get; set; }
        public virtual Property Property { get; set; }
    }
}
