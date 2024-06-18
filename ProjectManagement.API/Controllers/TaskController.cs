using System.Runtime.InteropServices.ComTypes;
using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BLL.DTOs.TaskDtos;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL.Enums;

namespace ProjectManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    
    /// <summary>
    /// Controller
    /// </summary>
    /// <param name="taskService"></param>
    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }


    /// <summary>
    /// Get All Tasks with filter and sorting
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetTasksFilter(ProgressStatus? status, int? priority, bool isPriorityAscending)
    {
        var results = await _taskService.GetTasksFilter(status, priority, isPriorityAscending);
        
        return Ok(results);
    }
    
    /// <summary>
    /// Get Task
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTask(int id)
    {
        var result  = await _taskService.GetTask(id);
        if (result == null)
        {
            return NotFound();
        }
        return Ok(result);
    }
    
    /// <summary>
    /// Create Task
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] TaskCreateDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdTask = await _taskService.CreateTask(request);
        return CreatedAtAction(nameof(GetTask), new { id = createdTask.Id }, createdTask);
    }
    
    /// <summary>
    /// Update Task by Id
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateTask([FromBody] TaskUpdateDto request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var result = await _taskService.GetTask(request.Id);
        
        if (result == null)
        {
            return NotFound();
        }

        await _taskService.UpdateTask(request);
        
        return NoContent();
    }

    /// <summary>
    /// Delete Task by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        var result = await _taskService.GetTask(id);
        
        if (result == null)
        {
            return NotFound();
        }
        
        await _taskService.DeleteTask(id);
        
        return NoContent(); 
    }
    




}