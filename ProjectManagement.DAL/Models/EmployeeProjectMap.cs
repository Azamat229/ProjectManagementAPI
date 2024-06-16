namespace ProjectManagement.DAL.Models;

public class ProjectEmployee
{
    public int Id { get; set; }
    public bool IsDeleted { get; set; }
    
    // Foreign key
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    
}