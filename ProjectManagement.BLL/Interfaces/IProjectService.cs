using ProjectManagement.BLL.DTOs;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BLL.Interfaces;

public interface IProjectService
{
    /// <summary>
    /// Get All Projects Async
    /// </summary>
    /// <returns></returns>
    Task<IEnumerable<ProjectGetDto>> GetProjects();
    
    /// <summary>
    /// Get Project By Id Async
    /// </summary>
    /// <param name="projectId"></param>
    /// <returns></returns>
    Task<ProjectGetDto> GetProject(int projectId);

    /// <summary>
    /// Get All Projects Async with filter and pagination
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    Task<IEnumerable<ProjectGetDto>> GetProjectsFilter(ProjectFilterDto filter);

    /// <summary>
    /// Create Project Async
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Project> CreateProject(ProjectCreateDto dto);

    /// <summary>
    /// Update Project Async
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    Task<Project> UpdateProject(ProjectUpdateDto dto);
    
    /// <summary>
    /// Delete Project Async
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteProject(int id);

    /// <summary>
    /// Add Employee To Project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    Task AddEmployeeToProject(int projectId, int employeeId);

    /// <summary>
    /// Remove Employee From Project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    Task RemoveEmployeeFromProject(int projectId, int employeeId);
}