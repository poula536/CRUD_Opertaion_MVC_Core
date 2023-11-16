using AutoMapper;
using Demo.DAL.Entities;
using Demo.PL.ViewModels;

namespace Demo.PL.Mappers
{
    public class Registerprofile : Profile
    {
        public Registerprofile()
        {
            CreateMap<RegisterViewModel , ApplicationUser>().ReverseMap();
        }
    }
}
