using ProjectManagement.DAL.Enums;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BLL.DTOs.TaskDtos;

public class TaskCreateDto
{
    public string Name { get; set; }
    
    public string Comments { get; set; }
    
    public int Priority { get; set; }
    
    public ProgressStatus Status { get; set; }
    
    public int AuthorId { get; set; }
    
    public int ImplementorId { get; set; }
    
    public int ProjectId { get; set; }

}