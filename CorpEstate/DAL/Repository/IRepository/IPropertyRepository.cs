using CorpEstate.BLL.Model;
using System.Linq.Expressions;

namespace CorpEstate.DAL.Repository.IRepository
{
    public interface IPropertyRepository : IRepository<Property>
    {
        //Task<List<Property>> GetAllAsync(Expression<Func<Property, bool>> filter = null);
        //Task<Property> GetAsync(Expression<Func<Property, bool>> filter = null, bool Tracked = true);
        //Task CreateAsync(Property entity);
        Task<Property> UpdateAsync(Property entity);
        //Task RemoveAsync(Property entity);
        //Task SaveAsync();
    }
}
