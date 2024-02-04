using AutoMapper;
using capstone_project_be.Application.DTOs;
using capstone_project_be.Domain.Entities;

namespace capstone_project_be.Application.AutoMapperProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<UserSignUpDTO, User>().ReverseMap();
            CreateMap<UserSignInDTO, User>().ReverseMap();
        }
    }
}
