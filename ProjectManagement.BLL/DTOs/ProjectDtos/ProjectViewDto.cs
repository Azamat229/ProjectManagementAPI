using ProjectManagement.BLL.DTOs.EmployeeDtos;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BLL.DTOs;

public class ProjectViewDto
{
    public int ProjectId { get; set; }
    public string Name { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndDatedAt { get; set; }
    public int Priority { get; set; }
    public string ProjectManagerName { get; set; }
    public string ClientCompanyName { get; set; }
    public string ContractorCompanyName { get; set; }
    public List<EmployeeViewDto> AssignedEmployees { get; set; }
}