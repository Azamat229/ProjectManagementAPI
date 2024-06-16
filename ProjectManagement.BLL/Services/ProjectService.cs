using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.BLL.DTOs;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BLL.Services;

public class ProjectService : IProjectService
{
    private readonly ProjectManagementContext _context;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Controller
    /// </summary>
    /// <param name="context"></param>
    public ProjectService(ProjectManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #region Public methods

    /// <summary>
    /// Retrieves all projects asynchronously.
    /// </summary>
    public async Task<IEnumerable<ProjectGetDto>>  GetProjects()
    {
        var projects =  await _context.Projects
            .Include(p => p.Employee)
            .Include(p => p.ClientCompany)
            .Include(p => p.ContractorCompany)
            .ToListAsync();


        return _mapper.Map<IEnumerable<ProjectGetDto>>(projects);

    }
    

    /// <summary>
    /// Retrieves a project by its ID asynchronously.
    /// </summary>
    public async Task<ProjectGetDto> GetProject(int projectId)
    {
        var project =  await _context.Projects
            .Include(p => p.Employee)
            .Include(p => p.ClientCompany)
            .Include(p => p.ContractorCompany)
            .FirstOrDefaultAsync(p => p.Id == projectId);


        return _mapper.Map<ProjectGetDto>(project);
    }

    /// <summary>
    /// Creates a new project asynchronously.
    /// </summary>
    public async Task<Project> CreateProject(ProjectCreateDto dto)
    {
        var project = new Project()
        {
            Name = dto.Name,
            StartedAt = dto.StartDate,
            EndAt = dto.StartDate,
            Priority = dto.Priority,
            ClientCompanyId = dto.ClientCompanyId,
            ContractorCompanyId =
                dto.ContractorCompanyId, //todo нужно чтобы во фронте нашли компанию и передали ID
            EmployeeId = dto.ProjectManagerId // todo тоже самое
        };
        
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    /// <summary>
    /// Updates an existing project asynchronously.
    /// </summary>
    public async Task<Project> UpdateProject(int id, ProjectUpdateDto dto)
    {
        var project = await _context.Projects.FindAsync(id);
        
        if (project == null)
        {
            return null;
        }
        
        project.Name = dto.Name;
        project.StartedAt = dto.StartDate;
        project.EndAt = dto.EndDate;
        project.Priority = dto.Priority;
        project.ClientCompanyId = dto.ClientCompanyId;
        project.ContractorCompanyId = dto.ContractorCompanyId;
        project.EmployeeId = dto.ProjectManagerId;
        
        _context.Entry(project).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return project;
    }

    /// <summary>
    /// Deletes a project by its ID asynchronously.
    /// </summary>
    public async Task<bool> DeleteProject(int id)
    {
        var project = await _context.Projects.FindAsync(id);
        if (project == null)
        {
            return false;
        }

        project.IsDeleted = true;
        
        _context.Entry(project).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
    
    /// <summary>
    ///  Retrieves all projects asynchronously including deleted 
    /// </summary>
    /// <returns></returns>
    public async Task<IEnumerable<Project>> GetProjectsIncludingDeleted()
    {
        return await _context.Projects
            .IgnoreQueryFilters() 
            .Include(p => p.Employee)
            .Include(p => p.ClientCompany)
            .Include(p => p.ContractorCompany)
            .ToListAsync();
    }
    
    /// <summary>
    /// Add Employee To Project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    public async Task AddEmployeeToProject(int projectId, int employeeId)
    {
        //todo обработку ошибки нужно поставить чтобы понять операция была правельная или нет

        var projectEmployee = new ProjectEmployee { ProjectId = projectId, EmployeeId = employeeId };
        _context.ProjectEmployees.Add(projectEmployee);
        await _context.SaveChangesAsync();
    }

    /// <summary>
    /// Remove Employee From Project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    public async Task RemoveEmployeeFromProject(int projectId, int employeeId)
    {
        var projectEmployee = await _context.
            ProjectEmployees
            .FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);
        
        //todo обработку ошибки нужно поставить чтобы понять операция была правельная или нет
        if (projectEmployee != null)
        {
            projectEmployee.IsDeleted = true;
        
            _context.Entry(projectEmployee).State = EntityState.Modified;
            _context.ProjectEmployees.Remove(projectEmployee);
            await _context.SaveChangesAsync();
        }
    }
    #endregion
    
}