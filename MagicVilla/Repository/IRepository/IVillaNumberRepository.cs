using MagicVilla.Model;
using System.Linq.Expressions;

namespace MagicVilla.Repository.IRepository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber entity);
    }
}

