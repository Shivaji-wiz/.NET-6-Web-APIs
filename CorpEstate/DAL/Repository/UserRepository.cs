using CorpEstate.BLL.Model;
using CorpEstate.DAL.Data;
using CorpEstate.DAL.Repository.IRepository;

namespace CorpEstate.DAL.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public UserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
