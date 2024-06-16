using ProjectManagement.DAL.Models;

namespace ProjectManagement.DAL;
using Microsoft.EntityFrameworkCore;

public class ProjectManagementContext : DbContext
{
    public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options) { }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ProjectEmployee> ProjectEmployees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        // Global query filter soft-delete
        modelBuilder.Entity<Project>().HasQueryFilter(c => !c.IsDeleted);
        
        #region Project 

        modelBuilder.Entity<Project>()
            .HasOne(x => x.Employee)
            .WithMany(x => x.Projects)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Project>()
            .HasOne(x => x.ClientCompany)
            .WithMany(x => x.ClientProjects)
            .HasForeignKey(x => x.ClientCompanyId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Project>()
            .HasOne(x => x.ContractorCompany)
            .WithMany(x => x.ContractorProjects)
            .HasForeignKey(x => x.ContractorCompanyId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion



        #region Project Employee 

        modelBuilder.Entity<ProjectEmployee>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<ProjectEmployee>()
            .HasOne(x => x.Employee)
            .WithMany(x => x.ProjectEmployees)
            .HasForeignKey(x => x.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<ProjectEmployee>()
            .HasOne(x => x.Project)
            .WithMany(x => x.ProjectEmployees)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
        
        
        base.OnModelCreating(modelBuilder);
    }
}