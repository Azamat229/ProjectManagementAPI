using Microsoft.AspNetCore.Mvc;
using ProjectManagement.BLL.DTOs.EmployeeDtos;
using ProjectManagement.BLL.Interfaces;

namespace ProjectManagement.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmployeeController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    /// <summary>
    /// Controller
    /// </summary>
    /// <param name="employeeService"></param>
    public EmployeeController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    #region Public methods
    
    /// <summary>
    /// Get All Employees
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetEmployees()
    {
        var employees = await _employeeService.GetEmployees();
        return Ok(employees);
    }

    /// <summary>
    /// Get Employee by Id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<IActionResult> GetEmployee(int id)
    {
        var employee = await _employeeService.GetEmployee(id);
        if (employee == null)
        {
            return NotFound();
        }
        return Ok(employee);
    }

    /// <summary>
    /// Create Employee
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] EmployeeCreateDto employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var createdEmployee = await _employeeService.CreateEmployee(employee);
        return CreatedAtAction(nameof(GetEmployee), new { id = createdEmployee.EmployeeId }, createdEmployee);
    }

    /// <summary>
    /// Update Employee
    /// </summary>
    /// <param name="id"></param>
    /// <param name="employee"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployee(int id, [FromBody] EmployeeUpdateDto employee)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }
        var employeeToUpdate = await _employeeService.GetEmployee(id);
        
        if (employeeToUpdate == null)
        {
            return NotFound();
        }

        await _employeeService.UpdateEmployee(employee);
        

        return NoContent();
    }
 
    /// <summary>
    /// Delete Employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployee(int id)
    {
        var employee = await _employeeService.GetEmployee(id);
        
        if (employee == null)
        {
            return NotFound();
        }
        
        await _employeeService.DeleteEmployee(id);
        
        return NoContent(); 
    }
    
    #endregion
}