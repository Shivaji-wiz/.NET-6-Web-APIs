using AutoMapper;
using CorpEstate.BLL.Model;
using CorpEstate.DAL.DTO;
using CorpEstate.DAL.Repository.IRepository;
using CorpEstate.DAL.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace CorpEstate.Controllers
{
    [ApiController]
    [Route("api/UserDetails")]
    public class UserDetailsController : ControllerBase
    {
        protected APIResponse _response;
        //private readonly ApplicationDbContext _db;
        private readonly IUserRepository _dbUser;
        private readonly IPropertyRepository _dbProp;
        private readonly IMapper _mapper;
        public UserDetailsController(IUserRepository dbUser, IPropertyRepository dbProp,IMapper mapper)
        {
            _dbUser = dbUser;
            _dbProp = dbProp;
            _mapper = mapper;
            _response = new APIResponse();
        }

        [HttpGet("GetUser")]
        public async Task<ActionResult<APIResponse>> GetUser(string email)
        {
            try
            {
                var user = await _dbUser.GetAsync(u => u.Email == email);
                _response.Result = _mapper.Map<UserDTO>(user);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetAllUsers")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> GetAllUsers()
        {
            try
            {
                IEnumerable<User> userList = await _dbUser.GetAllAsync();
                _response.Result = _mapper.Map<List<UserDTO>>(userList);
                _response.StatusCode = HttpStatusCode.OK;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet("GetUserProperties/{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<APIResponse>> GetAllUserProperty(int id)
        {
            try
            {
                IEnumerable<Property> userPropertyList = await _dbProp.GetAllAsync(u => u.UserID == id);
                _response.Result = _mapper.Map<List<PropertyDTO>>(userPropertyList);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete("DeleteUser/{id:int}")]
        [Authorize(Roles = "Admin,Buyer,Seller")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteUser(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var user = await _dbUser.GetAsync(u => u.Id == id);
                if (user == null)
                {
                    return NotFound();
                }

                //_db.Properties.Remove(property);
                //await _db.SaveChangesAsync();
                await _dbUser.RemoveAsync(user);
                _response.StatusCode = HttpStatusCode.NoContent;
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[HttpPost]
        //public async Task<ActionResult<APIResponse>> CreateUser([FromBody]UserCreateDTO NewUser)
        //{
        //    try
        //    {
        //        if (await _dbUser.GetAsync(u => u.Name.ToLower() == NewUser.Name) != null)
        //        {
        //            ModelState.AddModelError("Custom Error", "User already Exists!!");
        //            return BadRequest(ModelState);
        //        }

        //        if (NewUser == null)
        //        {
        //            return BadRequest();
        //        }

        //        User model = _mapper.Map<User>(NewUser);
        //        await _dbUser.CreateAsync(model);

        //        _response.Result = _mapper.Map<UserDTO>(model);
        //        _response.StatusCode = HttpStatusCode.Created;
        //        _response.IsSuccess = true;
        //        return CreatedAtRoute("GetUser", new { id = model.Id }, _response);
        //    }
        //    catch (Exception ex)
        //    {
        //        _response.IsSuccess = false;
        //        _response.ErrorMessages = new List<string>() { ex.ToString() };
        //    }
        //    return _response;
        //}
    }
}
