using Auth.Data;
using Auth.Model;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    [Route("api/AuthAPI")]
    [ApiController]

    public class AuthController : ControllerBase
    {

         UserData UserD = new UserData();

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return UserD.Users;
        }

        [HttpGet("{id:int}")]
        public ActionResult<User> getUser(int id)
        {
            var user = UserD.Users.FirstOrDefault(x => x.Id == id);
            return user;
    }
    }


}