using AutoMapper;
using CorpEstate.BLL.Model;
using CorpEstate.DAL.DTO;
using CorpEstate.DAL.Repository.IRepository;
using CorpEstate.DAL.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CorpEstate.Controllers
{
    [ApiController]
    [Route("api/Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        protected APIResponse _response;
        private readonly IPropertyRepository _dbProp;
        private readonly IMapper _mapper;

        public AdminController(IPropertyRepository dbProp, IMapper mapper)
        {
            _dbProp = dbProp;
            _mapper = mapper;
            this._response = new APIResponse();
        }

        [HttpGet("GetUnapprovedProperties")]
        public async Task<ActionResult<APIResponse>> GetProperties()
        {
            try
            {
                IEnumerable<Property> PropertyList = await _dbProp.GetAllAsync(u => u.Approved == false);
                _response.Result = _mapper.Map<List<PropertyDTO>>(PropertyList);
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

        [HttpPut("ApproveProperty")]
        public async Task<ActionResult<APIResponse>> ApproveProperty(int id, [FromBody] ApprovePropertyDTO ApproveProperty)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest();
                }

                var property = await _dbProp.GetAsync(u => u.Property_ID == id, Tracked: false);

                ApproveProperty.Property_ID = property.Property_ID;
                ApproveProperty.Property_Name = property.Property_Name;
                ApproveProperty.Property_Price = property.Property_Price;
                ApproveProperty.Property_Description = property.Property_Description;
                ApproveProperty.ImageUrl= property.ImageUrl;
                ApproveProperty.UserID = property.UserID;
                ApproveProperty.Seller_Name = property.Seller_Name;
                ApproveProperty.Seller_Contact = property.Seller_Contact;
                ApproveProperty.Property_ApprovedTime = DateTime.Now;
                ApproveProperty.Approved = true;

                Property model = _mapper.Map<Property>(ApproveProperty);
                //_db.Properties.Update(model);
                //await _db.SaveChangesAsync();

                await _dbProp.UpdateAsync(model);
                //_response.Result = model;
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        //[HttpDelete("{id:int}", Name = "RejectProperty")]
        [HttpDelete("RejectProperty/{id:int}")]
        public async Task<ActionResult<APIResponse>> RejectProperty(int id)
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

                await _dbProp.RemoveAsync(property);
                _response.IsSuccess = true;
                _response.StatusCode = HttpStatusCode.NoContent;
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
