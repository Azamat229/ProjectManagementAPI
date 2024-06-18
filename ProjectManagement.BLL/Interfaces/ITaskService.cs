using AutoMapper;
using ProjectManagement.BLL.DTOs.TaskDtos;
using ProjectManagement.DAL;
using ProjectManagement.DAL.Enums;

namespace ProjectManagement.BLL.Interfaces;

public interface ITaskService
{
    #region Public methods

    /// <summary>
    /// Retrieves all task asynchronously with filtering and sorting.
    /// </summary>
    /// <param name="status"></param>
    /// <param name="priority"></param>
    /// <param name="isPriorityAscending"></param>
    /// <returns></returns>
    Task<IEnumerable<TaskViewDto>> GetTasksFilter(ProgressStatus? status, int? priority, bool isPriorityAscending = true);


    /// <summary>
    /// Retrieves a task by its ID asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<TaskViewDto> GetTask(int id);


    /// <summary>
    /// Creates a new task asynchronously.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<TaskViewDto> CreateTask(TaskCreateDto request);

    /// <summary>
    /// Updates an existing task asynchronously.
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    Task<TaskViewDto> UpdateTask(TaskUpdateDto request);

    /// <summary>
    /// Deletes a task by its ID asynchronously.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteTask(int id);
    
    #endregion

}