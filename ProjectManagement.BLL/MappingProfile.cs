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
            .ForMember(dest => dest.ContractorCompanyName, opt => opt.MapFrom(src => src.ContractorCompany.Name))
            .ForMember(dest => dest.ProjectId, opt => opt.MapFrom(src => src.Id))
            
            .ForMember(dto => dto.AssignedEmployees, opt => opt.MapFrom(proj => proj.ProjectEmployees.Select(pe => new EmployeeGetDto()
            {
                EmployeeId = pe.Employee.Id,
                FirstName = pe.Employee.FirstName,
                LastName = pe.Employee.LastName,
                MiddleName = pe.Employee.MiddleName,
                Email = pe.Employee.Email
            })));
            
        
        #endregion

        #region Employee

                CreateMap<Employee, EmployeeGetDto>()
                    .ForMember(dest => dest.EmployeeId, opt => opt.MapFrom(src => src.Id));
                CreateMap<EmployeeCreateDto, Employee>();
                CreateMap<EmployeeUpdateDto, Employee>();

        #endregion

    }
    

}