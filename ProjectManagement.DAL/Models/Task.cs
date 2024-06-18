using ProjectManagement.DAL.Enums;

namespace ProjectManagement.DAL.Models;

public class Task
{
    /// <summary>
    /// Primary Key
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// Name of task
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Commentaries of task
    /// </summary>
    public string Comments { get; set; }
    
    /// <summary>
    /// Priority of task number 1,2,3,4,5
    /// </summary>
    public int Priority { get; set; }
    
    /// <summary>
    /// Progress  Status (0: To-Do 1: InProgress 2:Done )
    /// </summary>
    public ProgressStatus Status { get; set; }
    
    /// <summary>
    /// IsDeleted
    /// </summary>
    public bool IsDeleted { get; set; } = false;

    
    #region  Foreign keys

    /// <summary>
    /// Foreign key Employee.Id - Employee who create task
    /// </summary>
    public int AuthorId { get; set; }
    public Employee AuthorEmployee { get; set; }
    
    /// <summary>
    /// Foreign key Employee.Id - Employee who will do task
    /// </summary>
    public int ImplementorId { get; set; }
    public Employee ImplementorEmpoyee { get; set; }

    /// <summary>
    /// Foreign key Project.Id - Project which consists than task
    /// </summary>
    public int ProjectId { get; set; }
    public Project Project { get; set; }
    
    #endregion
 

}