using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.BLL.DTOs.TaskDtos;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL;
using ProjectManagement.DAL.Enums;

namespace ProjectManagement.BLL.Services;

public class TaskService : ITaskService
{
    private readonly ProjectManagementContext _context;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Controller
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public TaskService(ProjectManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    
    #region Public methods

    /// <summary>
    /// Retrieves all task asynchronously with filtering and sorting
    /// </summary>
    /// <param name="status"></param>
    /// <param name="priority"></param>
    /// <param name="isPriorityAscending"></param>
    /// <returns></returns>
    public async Task<IEnumerable<TaskViewDto>> GetTasksFilter(ProgressStatus? status, int? priority, bool isPriorityAscending = true)
    {
        var tasksQuery =  GetPrivateTasks();
        
        if (status.HasValue)
        {
            tasksQuery = tasksQuery.Where(c => c.Status == status);
        }
        
        if (status.HasValue)
        {
            tasksQuery = tasksQuery.Where(c => c.Priority == priority);
        }

        tasksQuery = isPriorityAscending
            ? tasksQuery.OrderBy(c => c.Priority)
            : tasksQuery.OrderByDescending(c => c.Priority);
        
        var taskList = await tasksQuery.ToListAsync();

        return  _mapper.Map<IEnumerable<TaskViewDto>>(taskList);
    }
    
    /// <summary>
    /// Retrieves a task by its ID asynchronously.
    /// </summary>
    public async Task<TaskViewDto> GetTask(int id)
    {
        var task = await GetPrivateTasks().FirstOrDefaultAsync(p => p.Id == id);
        
        return _mapper.Map<TaskViewDto>(task);
    }

    /// <summary>
    /// Creates a new task asynchronously.
    /// </summary>
    public async Task<TaskViewDto> CreateTask(TaskCreateDto request)
    {
        var task = new DAL.Models.Task
        {
            Name = request.Name,
            Comments = request.Comments,
            Priority = request.Priority,
            Status = request.Status,
            AuthorId = request.AuthorId,
            ImplementorId = request.ImplementorId,
            ProjectId = request.ProjectId
        };
        
        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();
        return _mapper.Map<TaskViewDto>(task);

    }

    /// <summary>
    /// Updates an existing task asynchronously.
    /// </summary>
    public async Task<TaskViewDto> UpdateTask(TaskUpdateDto request)
    {
        var task = await _context.Tasks.FindAsync(request.Id);
        
        if (task == null)
        {
            return null;
        }
        
        task.Name = request.Name;
        task.Comments = request.Comments;
        task.Priority = request.Priority;
        task.Status = request.Status;
        task.AuthorId = request.AuthorId;
        task.ImplementorId = request.ImplementorId;
        task.ProjectId = request.ProjectId;
        
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        
        return _mapper.Map<TaskViewDto>(task);
    }

    /// <summary>
    /// Deletes a task by its ID asynchronously.
    /// </summary>
    public async Task<bool> DeleteTask(int id)
    {
        var task = await _context.Tasks.FindAsync(id);
        
        if (task == null) return false;

        task.IsDeleted = true;
        
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
    
    #endregion

    
    #region Private methods
    
    private IQueryable<DAL.Models.Task> GetPrivateTasks()
    {
        var tasks =  _context.Tasks.AsQueryable();
        
        return tasks;
    }

    #endregion
}
