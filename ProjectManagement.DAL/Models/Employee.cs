using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.DAL.Models;

public class Employee
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// FirstName
    /// </summary>
    public string FirstName { get; set; }
    
    /// <summary>
    /// LastName
    /// </summary>
    public string LastName { get; set; }
    
    /// <summary>
    /// MiddleName
    /// </summary>
    public string MiddleName { get; set; }
    
    /// <summary>
    /// Email
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    /// IsDeleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    /// <summary>
    /// Projects
    /// </summary>
    public ICollection<Project> Projects { get; set; } = new List<Project>();
    
    
    /// <summary>
    /// ProjectEmployees
    /// </summary>
    public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();
    
    /// <summary>
    /// Task
    /// </summary>
    public ICollection<Task> Task { get; set; } = new List<Task>();
    
}