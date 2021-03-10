using WidePictBoard.Application.Common;
using WidePictBoard.Application.Identity.Contracts;
using WidePictBoard.Application.Services.User.Contracts;
using AutoMapper;

namespace WidePictBoard.Application.MapProfiles
{
    public class UserMapProfile : Profile
    {
        public UserMapProfile()
        {
            CreateMap<CreateUser.Response, Register.Response>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId));

            CreateMap<Register.Request, CreateUser.Request>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.Username))
                //.ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                //.ForMember(dest => dest.Password, opt => opt.MapFrom(src => src.Password))
                .ForMember(dest => dest.Role, opt => opt. MapFrom(src => RoleConstants.UserRole))
                ;
        }
    }
}