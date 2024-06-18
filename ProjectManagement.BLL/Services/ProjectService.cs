using System.Security.AccessControl;
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
    /// <param name="mapper"></param>
    public ProjectService(ProjectManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    #region Public methods

    /// <summary>
    /// Retrieves all projects asynchronously.
    /// </summary>
    public async Task<IEnumerable<ProjectViewDto>>  GetProjects()
    {
        var projectsQuery =  GetPrivateProjects();
        
        var projects = await projectsQuery.ToListAsync();

        return _mapper.Map<IEnumerable<ProjectViewDto>>(projects);
    }
    
    /// <summary>
    /// Retrieves a project by its ID asynchronously.
    /// </summary>
    public async Task<ProjectViewDto> GetProject(int projectId)
    {
        var project = await GetPrivateProjects().FirstOrDefaultAsync(p => p.Id == projectId);
        
        return _mapper.Map<ProjectViewDto>(project);
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
            ContractorCompanyId = dto.ContractorCompanyId, 
            EmployeeId = dto.ProjectManagerId 
        };
        
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        return project;
    }

    /// <summary>
    /// Updates an existing project asynchronously.
    /// </summary>
    public async Task<Project> UpdateProject(ProjectUpdateDto request)
    {
        var project = await _context.Projects.FindAsync(request.Id);
        
        if (project == null)
        {
            return null;
        }
        
        project.Name = request.Name;
        project.StartedAt = request.StartDate;
        project.EndAt = request.EndDate;
        project.Priority = request.Priority;
        project.ClientCompanyId = request.ClientCompanyId;
        project.ContractorCompanyId = request.ContractorCompanyId;
        project.EmployeeId = request.ProjectManagerId;
        
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
        
        if (project == null) return false;

        project.IsDeleted = true;
        
        _context.Entry(project).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Add Employee To Project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    public async Task<bool>  AddEmployeeToProject(int projectId, int employeeId)
    {
        //todo обработку ошибки нужно поставить чтобы понять операция была правельная или нет

        var projectEmployee = new ProjectEmployee 
            { 
                ProjectId = projectId, 
                EmployeeId = employeeId 
            };
        
        _context.ProjectEmployees.Add(projectEmployee);
        await _context.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Remove Employee From Project
    /// </summary>
    /// <param name="projectId"></param>
    /// <param name="employeeId"></param>
    public async Task<bool>  RemoveEmployeeFromProject(int projectId, int employeeId)
    {
        var projectEmployee = await _context.
            ProjectEmployees
            .FirstOrDefaultAsync(pe => pe.ProjectId == projectId && pe.EmployeeId == employeeId);
        
        //todo обработку ошибки нужно поставить чтобы понять операция была правельная или нет
        if (projectEmployee != null)
        {
            projectEmployee.IsDeleted = true;
        
            _context.Entry(projectEmployee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        return true;
    }

    /// <summary>
    /// Get Project Filter
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public async Task<IEnumerable<ProjectViewDto>> GetProjectsFilter( ProjectFilterDto filter)
    {
        var projectsQuery = GetPrivateProjects();

        var projectQueryFiltered =  ApplyFilters(filter, projectsQuery);

        projectQueryFiltered = ApplySorting(filter, projectQueryFiltered);

        var projectPaginated = Paginate(projectQueryFiltered, filter.PageNumber, filter.PageSize);

        return  _mapper.Map<IEnumerable<ProjectViewDto>>(projectPaginated);

        // если параметр не null то просто верни если dto существует то отсортеруй
    }

    #endregion

    
    #region Private methods

    private  IQueryable<Project> GetPrivateProjects()
    {
        var projects =  _context.Projects
            .Include(p => p.Employee)
            .Include(p => p.ClientCompany)
            .Include(p => p.ContractorCompany)
            .Include(p => p.ProjectEmployees)
            .ThenInclude(p => p.Employee);
        
        return projects;
    }
    
    private IQueryable<Project> ApplyFilters(ProjectFilterDto filter, IQueryable<Project> query)
    {
        if (filter.StartDateFrom.HasValue)
        {
            query = query.Where(c => c.StartedAt >= filter.StartDateFrom.Value);
        }

        if (filter.StartDateTo.HasValue)
        {
            query = query.Where(c => c.StartedAt <= filter.StartDateTo.Value);
        }

        if (filter.Priority.HasValue)
        {
            query = query.Where(c => c.Priority == filter.Priority.Value);
        }

        if (!string.IsNullOrWhiteSpace(filter.ProjectManagerName))
        {
            query = query.Where(c =>
                c.Employee.FirstName + " " + c.Employee.LastName ==
                filter.ProjectManagerName); //todo нужно улучшить, Ajax если не получилось то лучше убрать 
        }

        if (!string.IsNullOrWhiteSpace(filter.ProjectName))
        {
            query = query.Where(c => c.Name.Contains(filter.ProjectName));
        }

        return query;
    }

    private IQueryable<Project> ApplySorting(ProjectFilterDto filter, IQueryable<Project> query)
    {
        var projectQuerySorted = filter.SortBy.ToLower() switch
        {
            "name" => filter.IsAscending
                ? query.OrderBy(c => c.Name)
                : query.OrderByDescending(c => c.Name),
            "startdate" => filter.IsAscending
                ? query.OrderBy(c => c.StartedAt)
                : query.OrderByDescending(c => c.StartedAt),
            "priority" => filter.IsAscending
                ? query.OrderBy(c => c.Priority)
                : query.OrderByDescending(c => c.Priority),
            _ => query.OrderBy(c => c.Name)
        };

        return projectQuerySorted;
    }
    
    private async Task<List<Project>> Paginate(IQueryable<Project> query, int pageNumber, int pageSize)
    {
        return await query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    } 

    #endregion
}