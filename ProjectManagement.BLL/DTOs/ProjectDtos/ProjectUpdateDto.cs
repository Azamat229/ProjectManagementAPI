namespace ProjectManagement.BLL.DTOs;

public class ProjectUpdateDto
{
    public string Name { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Priority { get; set; }
    public int ClientCompanyId { get; set; }
    public int ContractorCompanyId { get; set; }
    public int ProjectManagerId { get; set; }
}