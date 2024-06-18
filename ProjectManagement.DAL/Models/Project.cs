namespace ProjectManagement.DAL.Models;

public class Project
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name of Project
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Started date
    /// </summary>
    public DateTime StartedAt { get; set; }
    
    /// <summary>
    /// End date
    /// </summary>
    public DateTime? EndAt { get; set; }
    
    /// <summary>
    /// Priority number 1,2,3,4,5
    /// </summary>
    public int Priority { get; set; }
    
    /// <summary>
    /// Is Deleted 
    /// </summary>
    public bool IsDeleted { get; set; } 

    
    //Foreign key
    
    /// <summary>
    /// Foreign key Company.Id - Client Company
    /// </summary>
    public int ClientCompanyId { get; set; }
    public Company ClientCompany { get; set; } 
    
    /// <summary>
    /// Foreign key Company.Id - Contract Company
    /// </summary>
    public int ContractorCompanyId { get; set; }
    public Company ContractorCompany { get; set; }

    /// <summary>
    /// Foreign key Employee.Id - Project manager
    /// </summary>
    public int EmployeeId { get; set; }
    public Employee Employee { get; set; }
    
    // Navigation properties
    public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    
    public ICollection<Task> Task { get; set; } = new List<Task>();



}