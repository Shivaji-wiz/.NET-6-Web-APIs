using AutoMapper;
using CorpEstate.BLL.Model;
using CorpEstate.DAL.DTO;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CorpEstate.Infrastructure
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Property, PropertyDTO>().ReverseMap();
            CreateMap<Property, CreatePropertyDTO>().ReverseMap();
            CreateMap<Property, UpdatePropertyDTO>().ReverseMap();
            CreateMap<Property, ApprovePropertyDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
            CreateMap<User, UserCreateDTO>().ReverseMap();
            CreateMap<User, UserLoginDTO>().ReverseMap();
            CreateMap<User, User>().ReverseMap();
            CreateMap<User, UpdateUserDTO>().ReverseMap();  
            CreateMap<PropertyReview, PropertyReviewDTO>().ReverseMap();
            CreateMap<PropertyReview, CreatePropertyReviewDTO>().ReverseMap();
        }
    }
}
