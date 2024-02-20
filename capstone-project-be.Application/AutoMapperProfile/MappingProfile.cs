using AutoMapper;
using capstone_project_be.Application.DTOs.Auths;
using capstone_project_be.Application.DTOs.Regions;
using capstone_project_be.Application.DTOs.Users;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //User
            CreateMap<UserSignUpDTO, User>().ReverseMap();
            CreateMap<UserSignInDTO, User>().ReverseMap();
            CreateMap<ResetPasswordVerificationDTO, User>().ReverseMap();
            CreateMap<ResetPasswordDTO, User>().ReverseMap();
            CreateMap<UpdateProfileDTO, User>().ReverseMap();
            CreateMap<GoogleAuthDTO, User>().ReverseMap();
            CreateMap<UserDTO, User>().ReverseMap();

            //Region
            CreateMap<RegionDTO, Region>().ReverseMap();
            CreateMap<UpdateRegionDTO, Region>().ReverseMap();
        }
    }
}
