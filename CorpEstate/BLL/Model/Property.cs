using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;

namespace CorpEstate.BLL.Model
{
    public class Property
    {
        [Key]
        public int Property_ID { get; set; }
        [Required]
        public string Property_Name { get; set; }
        [Required]
        public string Property_Price { get; set; }
        [Required]
        public string Property_Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        [Required]
        public DateTime Property_CreatedTime { get; set; }
        public DateTime Property_UpdatedTime { get; set; }

        [ForeignKey("User")]
        public int UserID { get; set; }
        public User User { get; set; }

        [Required]
        public string Seller_Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string Seller_Contact { get; set; }

        public bool Approved { get; set; }


    }
}
