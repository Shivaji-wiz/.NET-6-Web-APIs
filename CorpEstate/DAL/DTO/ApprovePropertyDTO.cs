using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CorpEstate.DAL.DTO
{
    public class ApprovePropertyDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Property_ID { get; set; }
        [Required]
        public string Property_Name { get; set; }
        [Required]
        public string Property_Price { get; set; }
        [Required]
        public string Property_Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public DateTime Property_ApprovedTime { get; set; }

        public int UserID { get; set; }
        [Required]
        public string Seller_Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string Seller_Contact { get; set; }

        public bool Approved { get; set; }
    }
}
