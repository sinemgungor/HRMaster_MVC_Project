using AutoMapper;
using HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminDTOs;
using HRMaster_MVC_Project.Models.ViewModels;

namespace HRMaster_MVC_Project.Mapper
{
    public class CompanyMapper:Profile
    {
        public CompanyMapper()
        {
            CreateMap<CompanyVM, AddCompanyDTO>()
     .ForMember(dest => dest.LogoPath, opt => opt.MapFrom(src => src.LogoPath))// CompanyAddress dahil
     .ReverseMap();
        }

    }
}
