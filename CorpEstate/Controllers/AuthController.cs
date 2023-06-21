﻿using AutoMapper;
using CorpEstate.BLL.Model;
using CorpEstate.DAL.DTO;
using CorpEstate.DAL.Repository.IRepository;
using CorpEstate.Services.IService;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Cryptography;

namespace CorpEstate.Controllers
{
    [ApiController]
    [Route("api/Auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _dbUser;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        protected APIResponse _response;

        public AuthController(IUserRepository dbUser, IMapper mapper, IJwtService jwtService)
        {
            _dbUser = dbUser;
            _mapper = mapper;
            _jwtService = jwtService;
            this._response = new APIResponse();
        }

        [HttpPost("Register_User")]
        public async Task<ActionResult<User>> Register(UserCreateDTO newUser)
        {
            _jwtService.CreatePasswordHash(newUser.Password, out byte[] passwordHash, out byte[] passwordSalt);

            User model = _mapper.Map<User>(newUser);
            model.PasswordSalt = passwordSalt;
            model.PasswordHash = passwordHash;

            await _dbUser.CreateAsync(model);
            return Ok(model);   
            
        }

        [HttpPut("Forgot_Password")]
        public async Task<ActionResult<APIResponse>> UpdatePassword(int id,[FromBody] UpdateUserDTO updateData)
        {
            try
            {
                if (id == 0 || id != updateData.Id)
                {
                    return BadRequest();
                }

                var user = await _dbUser.GetAsync(u => u.Id == id, Tracked: false);

                _jwtService.CreatePasswordHash(updateData.Password, out byte[] passwordHash, out byte[] passwordSalt);

                User model = _mapper.Map<User>(updateData);
                model.PasswordHash=passwordHash;
                model.PasswordSalt = passwordSalt;
                model.Email = user.Email;
                model.CreatedTime = user.CreatedTime;
                model.Id= user.Id;
                model.RoleName = user.RoleName;
                model.Name= user.Name;
                //_db.Properties.Update(model);
                //await _db.SaveChangesAsync();
                await _dbUser.UpdateAsync(model);
                _response.Result = model;
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
    

        [HttpPost("Login_User")]
        public async Task<ActionResult<string>> Login(UserLoginDTO request)
        {
            if(request.Name == "AdminA" && request.Password == "AdminA@123")
            {
                var model = _mapper.Map<User>(request);
                string Token = _jwtService.CreateAdminToken(model);
                //return Ok(Token);
                return Ok(new
                {
                    Token = Token,
                    Message = "Login Success!"
                });
            }
            else
            {
                var user = await _dbUser.GetAsync(u => u.Name == request.Name);

                if (user == null)
                {
                    return BadRequest("User Not found");
                }

                //var model = _mapper.Map<User>(user);

                if (!_jwtService.verifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
                {
                    return BadRequest("Password Invalid");
                }
                string Token = _jwtService.CreateUserToken(user);
                //return Ok(Token);
                return Ok(new
                {
                    Token = Token,
                    Message = "Login Success!"
                });
                //return Ok("Token valid!!");
            }
        }
    }
}
