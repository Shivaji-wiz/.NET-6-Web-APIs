using CorpEstate.BLL.Model;
using CorpEstate.DAL.Data;
using CorpEstate.DAL.Repository.IRepository;

namespace CorpEstate.DAL.Repository
{
    public class PropertyReviewRepository: Repository<PropertyReview> ,IPropertyReviewRepository
    {
        private readonly ApplicationDbContext _db;
        public PropertyReviewRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
