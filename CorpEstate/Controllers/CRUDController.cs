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
    [Route("api/CRUDController")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        //private readonly ApplicationDbContext _db;
        protected APIResponse _response;
        private readonly IPropertyRepository _dbProp;
        private readonly IMapper _mapper;

        public CRUDController(IPropertyRepository dbProp, IMapper mapper)
        {
            _dbProp = dbProp;
            _mapper = mapper;
            this._response = new APIResponse();
        }

        [HttpGet]
        public async Task<ActionResult<APIResponse>> GetProperties()
        {
            try
            {
                IEnumerable<Property> PropertyList = await _dbProp.GetAllAsync(u => u.Approved != false);
                _response.Result = _mapper.Map<List<PropertyDTO>>(PropertyList);
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

        [HttpGet("{id:int}",Name = "GetProperty")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetProperty(int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var property = await _dbProp.GetAsync(u => u.Property_ID == id);

                if (property == null)
                {
                    return NotFound();
                }
                if (property.Approved == false)
                {
                    ModelState.AddModelError("Custom Error", "Property Isn't approved by Admin!!");
                    return BadRequest(ModelState);
                }

                _response.Result = _mapper.Map<PropertyDTO>(property);
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

        [HttpPost]
        [Authorize(Roles = "Admin,Buyer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateProperty([FromBody] CreatePropertyDTO CreateProperty)
        {
            try
            {
                if (await _dbProp.GetAsync(u => u.Property_Name.ToLower() == CreateProperty.Property_Name) != null)
                {
                    ModelState.AddModelError("Custom Error", "Property already Exists!!");
                    return BadRequest(ModelState);
                }

                if (CreateProperty == null)
                {
                    return BadRequest(CreateProperty);
                }

                Property property = _mapper.Map<Property>(CreateProperty);
                property.Approved = false;

                //_db.Properties.AddAsync(model);
                //await _db.SaveChangesAsync();
                await _dbProp.CreateAsync(property);
                _response.Result = _mapper.Map<PropertyDTO>(property);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.OK;

                return CreatedAtRoute("GetProperty", new { id = property.Property_ID }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        [Authorize(Roles = "Admin,Buyer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVilla(int id, [FromBody]UpdatePropertyDTO UpdateProperty)
        {
            try
            {
                if (id == 0 || id != UpdateProperty.Property_ID)
                {
                    return BadRequest();
                }

                Property model = _mapper.Map<Property>(UpdateProperty);
                //_db.Properties.Update(model);
                //await _db.SaveChangesAsync();
                await _dbProp.UpdateAsync(model);
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

        [HttpDelete("{id:int}",Name = "DeleteProperty")]
        [Authorize(Roles = "Admin,Buyer")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteProperty(int id)
        {
            try
            {
                if (id == null)
                {
                    return BadRequest();
                }

                var property = await _dbProp.GetAsync(u => u.Property_ID == id);
                if (property == null)
                {
                    return NotFound();
                }

                //_db.Properties.Remove(property);
                //await _db.SaveChangesAsync();
                await _dbProp.RemoveAsync(property);
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
    }
}
