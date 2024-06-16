namespace ProjectManagement.DAL.Models;

public class Project
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime StartedAt { get; set; }
    public DateTime? EndAt { get; set; }
    public int Priority { get; set; }
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



}