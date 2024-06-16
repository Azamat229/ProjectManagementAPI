using AutoMapper;
using ProjectManagement.BLL.DTOs;
using ProjectManagement.BLL.DTOs.EmployeeDtos;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BLL;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        #region Project
        
        CreateMap<Project, ProjectGetDto>()
            .ForMember(dest => dest.ProjectManagerName, opt => opt.MapFrom(src => src.Employee.FirstName + " "+ src.Employee.LastName))
            .ForMember(dest => dest.ClientCompanyName, opt => opt.MapFrom(src => src.ClientCompany.Name))
            .ForMember(dest => dest.ContractorCompanyName, opt => opt.MapFrom(src => src.ContractorCompany.Name));

        #endregion

        #region Employee

                CreateMap<Employee, EmployeeGetDto>();
                CreateMap<EmployeeCreateDto, Employee>();
                CreateMap<EmployeeUpdateDto, Employee>();

        #endregion

    }
    

}