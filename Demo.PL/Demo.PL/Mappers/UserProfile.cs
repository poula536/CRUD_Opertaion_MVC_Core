using AutoMapper;
using Demo.DAL.Entities;
using Demo.PL.ViewModels;

namespace Demo.PL.Mappers
{
	public class UserProfile : Profile
	{
        public UserProfile()
        {
			CreateMap<UserViewModel, ApplicationUser>().ReverseMap();

		}
	}
}
