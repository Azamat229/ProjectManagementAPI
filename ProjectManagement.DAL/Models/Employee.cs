using System.ComponentModel.DataAnnotations;

namespace ProjectManagement.DAL.Models;

public class Employee
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string Email { get; set; }
    public bool IsDeleted { get; set; }

    public ICollection<Project> Projects { get; set; } = new List<Project>();
    public ICollection<ProjectEmployee> ProjectEmployees { get; set; } = new List<ProjectEmployee>();

}