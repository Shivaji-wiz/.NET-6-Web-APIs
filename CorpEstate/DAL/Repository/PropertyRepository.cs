using CorpEstate.BLL.Model;
using CorpEstate.DAL.Data;
using CorpEstate.DAL.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace CorpEstate.DAL.Repository
{
    public class PropertyRepository : Repository<Property>, IPropertyRepository
    {
        private readonly ApplicationDbContext _db;
        public PropertyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Property> UpdateAsync(Property entity)
        {
            entity.Property_UpdatedTime = DateTime.Now;
            _db.Properties.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
