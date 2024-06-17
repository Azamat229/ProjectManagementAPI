using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BLL.DTOs;
using ProjectManagement.BLL.Interfaces;

namespace ProjectManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProjectController : ControllerBase
{

    private readonly IProjectService _projectService;
    
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="projectService"></param>
    public ProjectController(IProjectService projectService)
    {
        _projectService = projectService;
    }

    /// <summary>
    /// Get All Projects
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetProjects()
    {
        var projects = await _projectService.GetProjects();
        return Ok(projects);
    }

    /// <summary>
    /// Get Project
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProject(int id)
    {
        var project = await _projectService.GetProject(id);
        if (project == null)
        {
            return NotFound();
        }
        return Ok(project);
    }
    
    /// <summary>
    /// Get All Projects by filter and pagination
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    [HttpPost("filter")]
    public async Task<IActionResult> GetProjectFilter([FromBody] ProjectFilterDto filter)
    {
        var projects = await _projectService.GetProjectsFilter(filter);
        
        if (projects == null)
        {
            return NotFound();
        }
        return Ok(projects);
    }

    /// <summary>
    /// Create Project
    /// </summary>
    /// <param name="project"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] ProjectCreateDto project)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdProject = await _projectService.CreateProject(project);
        return CreatedAtAction(nameof(GetProject), new { id = createdProject.Id }, createdProject);
    }

    /// <summary>
    /// Update Project by Id
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateProject([FromBody] ProjectUpdateDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var projectToUpdate = await _projectService.GetProject(request.Id);
        
        if (projectToUpdate == null)
        {
            return NotFound();
        }

        await _projectService.UpdateProject(request);
        

        return NoContent();
    }

    /// <summary>
    /// Delete Project by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        var project = await _projectService.GetProject(id);
        
        if (project == null)
        {
            return NotFound();
        }
        
        await _projectService.DeleteProject(id);
        
        return NoContent(); 
    }

    /// <summary>
    /// Add Employee To Project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    [HttpPost("{projectId}/add-employee/{employeeId}")]  //todo в будущем изменить на объект 
    public async Task<IActionResult> AddEmployeeToProject(int projectId, int employeeId)
    {
        await _projectService.AddEmployeeToProject(projectId, employeeId);
        return Ok();
    }

    /// <summary>
    /// Remove Employee From Project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    /// <returns></returns>
    [HttpDelete("{projectId}/remove-employee/{employeeId}")] //todo в будущем изменить на объект 
    public async Task<IActionResult> RemoveEmployeeFromProject(int employeeId, int projectId)
    {
        await _projectService.RemoveEmployeeFromProject(projectId, employeeId);
        return Ok();
    }
    
    
}