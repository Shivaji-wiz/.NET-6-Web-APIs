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
    [Route("api/Crud")]
    [ApiController]
    public class CRUDController : ControllerBase
    {
        //private readonly ApplicationDbContext _db;
        protected APIResponse _response;
        private readonly IPropertyRepository _dbProp;
        private readonly IPropertyReviewRepository _dbPropRe;
        private readonly IMapper _mapper;

        public CRUDController(IPropertyRepository dbProp,IPropertyReviewRepository dbPropRe ,IMapper mapper)
        {
            _dbProp = dbProp;
            _dbPropRe = dbPropRe;
            _mapper = mapper;
            this._response = new APIResponse();
        }

        [HttpGet("GetAllProperties")]
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

        [HttpGet("{id:int}", Name = "GetProperty")]
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

        [HttpPost("CreateNewProperty")]
        [Authorize(Roles = "Admin,Seller")]
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
                property.Property_CreatedTime= DateTime.UtcNow;
                property.Approved = false;

                //_db.Properties.AddAsync(model);
                //await _db.SaveChangesAsync();
                await _dbProp.CreateAsync(property);
                _response.Result = _mapper.Map<PropertyDTO>(property);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetProperty", new { id = property.Property_ID }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut("UpdateExistingProperty/{id:int}")]
        [Authorize(Roles = "Admin,Seller")]
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
                model.Property_UpdatedTime = DateTime.Now;
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

        [HttpDelete("DeleteProperty/{id:int}")]
        [Authorize(Roles = "Admin,Seller")]
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

        [HttpGet("GetPropertyReviews/{id:int}")]
        public async Task<ActionResult<APIResponse>> GetPropertyReviews(int id)
        {
            try
            {
                IEnumerable<PropertyReview> propertyReviews = await _dbPropRe.GetAllAsync(u => u.Property_Id == id);
                _response.Result = _mapper.Map<List<PropertyReviewDTO>>(propertyReviews);
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

        [HttpPost("CreateNewPropertyReview")]
        [Authorize(Roles = "Admin,Seller,Buyer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddPropertyReview([FromBody] CreatePropertyReviewDTO newReview)
        {
            try
            {
                if (newReview == null)
                {
                    return BadRequest(newReview);
                }

                PropertyReview review = _mapper.Map<PropertyReview>(newReview);

                //_db.Properties.AddAsync(model);
                //await _db.SaveChangesAsync();
                await _dbPropRe.CreateAsync(review);
                _response.Result = _mapper.Map<PropertyReviewDTO>(review);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.Created;

                //return CreatedAtRoute("GetProperty", new { id = property.Property_ID }, _response);
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
