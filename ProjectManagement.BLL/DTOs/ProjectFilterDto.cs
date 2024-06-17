namespace ProjectManagement.BLL.DTOs;

public class ProjectFilterDto
{
    public DateTime? StartDateFrom { get; set; }
    public DateTime? StartDateTo { get; set; }
    public int? Priority { get; set; }
    public string ProjectManagerName { get; set; }
    public string ProjectName { get; set; }
    public string SortBy { get; set; } = "Name";
    public bool IsAscending { get; set; } = true;
    public int PageNumber { get; set; } = 1; 
    public int PageSize { get; set; } = 10;
}