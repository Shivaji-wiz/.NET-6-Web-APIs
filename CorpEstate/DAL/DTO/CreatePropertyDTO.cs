using CorpEstate.BLL.Model;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CorpEstate.DAL.DTO
{
    public class CreatePropertyDTO
    {
        [Required]
        public string Property_Name { get; set; }
        [Required]
        public string Property_Price { get; set; }
        [Required]
        public string Property_Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public DateTime Property_CreatedTime { get; set; }
        [Required]
        public DateTime Property_UpdatedTime { get; set; }

        public int UserID { get; set; }
        [Required]
        public string Seller_Name { get; set; }
        [Required]
        [MaxLength(10)]
        public string Seller_Contact { get; set; }

        public bool Approved { get; set; }
    }
}
