using CorpEstate.BLL.Model;

namespace CorpEstate.DAL.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> UpdateAsync(User entity);
    }
}