namespace ProjectManagement.DAL.Models;

public class Company
{
    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsDeleted { get; set; }

    
    // Navigation properties

    /// <summary>
    /// Client  Projects 
    /// </summary>
    public ICollection<Project> ClientProjects { get; set; } = new List<Project>();
    
    /// <summary>
    /// Contract Project
    /// </summary>
    public ICollection<Project> ContractorProjects { get; set; } = new List<Project>();

    /// <summary>
    /// ProjectEmployeeMap 
    /// </summary>
    public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    
    

}