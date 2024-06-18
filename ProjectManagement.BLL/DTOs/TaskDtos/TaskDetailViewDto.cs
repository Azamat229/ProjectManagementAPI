using ProjectManagement.BLL.DTOs.EmployeeDtos;
using ProjectManagement.DAL.Enums;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BLL.DTOs.TaskDtos;

public class TaskDetailViewDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Comments { get; set; }
    
    public int Priority { get; set; }
    
    public ProgressStatus Status { get; set; }
    
    public EmployeeViewDto Author { get; set; }
    
    public EmployeeViewDto Implementor { get; set; }
    
    public ProjectViewDto Project { get; set; }
}