using ProjectManagement.BLL.DTOs.EmployeeDtos;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BLL.Interfaces;

public interface IEmployeeService
{
    #region Public Methods

    /// <summary>
    /// Get All Employees Async
    /// </summary>
    /// <returns></returns>
    Task<List<EmployeeViewDto>> GetEmployees();
    
    /// <summary>
    /// Get Employee By Id Async
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<EmployeeViewDto> GetEmployee(int id);
    
    /// <summary>
    /// Create Employee Async
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    Task<EmployeeViewDto> CreateEmployee(EmployeeCreateDto employee);
    
    /// <summary>
    /// Update Employee Async
    /// </summary>
    /// <param name="employee"></param>
    /// <returns></returns>
    Task<EmployeeViewDto> UpdateEmployee(EmployeeUpdateDto employee);
    
    /// <summary>
    /// Delete Employee Async
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    Task<bool> DeleteEmployee(int id);
    
    #endregion

}