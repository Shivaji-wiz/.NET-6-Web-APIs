using CorpEstate.BLL.Model;

namespace CorpEstate.Services.IService
{
    public interface IJwtService
    {
        string CreateUserToken(User user);
        string CreateAdminToken(User user);
        void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt);
        bool verifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt);
    }
}
