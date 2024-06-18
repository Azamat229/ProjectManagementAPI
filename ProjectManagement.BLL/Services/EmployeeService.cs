using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.BLL.DTOs.EmployeeDtos;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.DAL;
using ProjectManagement.DAL.Models;

namespace ProjectManagement.BLL.Services;

public class EmployeeService : IEmployeeService
{
    private readonly ProjectManagementContext _context;
    private readonly IMapper _mapper;

    #region Public methods

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="mapper"></param>
    public EmployeeService(ProjectManagementContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    /// <summary>
    /// Get Employees
    /// </summary>
    /// <returns></returns>
    public async Task<List<EmployeeViewDto>> GetEmployees()
    {
        var employees = await _context.Employees.ToListAsync();
        return _mapper.Map<List<EmployeeViewDto>>(employees);
    }

    /// <summary>
    /// Get Employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<EmployeeViewDto> GetEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null) return null;
        return _mapper.Map<EmployeeViewDto>(employee);
    }

    /// <summary>
    /// Create Employee 
    /// </summary>
    /// <param name="employeeCreateDto"></param>
    /// <returns></returns>
    public async Task<EmployeeViewDto> CreateEmployee(EmployeeCreateDto employeeCreateDto)
    {
        var employee = _mapper.Map<Employee>(employeeCreateDto);
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<EmployeeViewDto>(employee);
    }

    /// <summary>
    /// Update Employee
    /// </summary>
    /// <param name="employeeUpdateDto"></param>
    /// <returns></returns>
    public async Task<EmployeeViewDto> UpdateEmployee(EmployeeUpdateDto employeeUpdateDto)
    {
        var employee = await _context.Employees.FindAsync(employeeUpdateDto.EmployeeId);
        if (employee == null) return null;
        _mapper.Map(employeeUpdateDto, employee);
        await _context.SaveChangesAsync();
        return _mapper.Map<EmployeeViewDto>(employee);
    }

    /// <summary>
    /// Delete Employee
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<bool> DeleteEmployee(int id)
    {
        var employee = await _context.Employees.FindAsync(id);
        if (employee == null)
        {
            return false;
        }

        employee.IsDeleted = true;
        
        _context.Entry(employee).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return true;
    }
    
    #endregion

}