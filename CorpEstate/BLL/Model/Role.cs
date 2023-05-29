using System.ComponentModel.DataAnnotations;

namespace CorpEstate.BLL.Model
{
    public class Role
    {
        [Key]
        public int Role_Id { get; set; }
        public string Role_Name { get; set; }
    }
}
