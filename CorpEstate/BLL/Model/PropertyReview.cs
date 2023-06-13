using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorpEstate.BLL.Model
{
    public class PropertyReview
    {
        [Key]
        public int ReviewId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        [ForeignKey("Property")]
        public int Property_Id { get; set; }
        public virtual Property Property { get; set; }
    }
}
