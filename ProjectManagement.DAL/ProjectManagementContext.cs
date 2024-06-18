using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ProjectManagement.DAL.Models;
using Task = ProjectManagement.DAL.Models.Task;

namespace ProjectManagement.DAL;
using Microsoft.EntityFrameworkCore;

public class ProjectManagementContext : DbContext
{
    public ProjectManagementContext(DbContextOptions<ProjectManagementContext> options) : base(options) { }
    
    public DbSet<Project> Projects { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<ProjectEmployee> ProjectEmployees { get; set; }
    public DbSet<Models.Task> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        base.OnModelCreating(modelBuilder);
        // Global query filter soft-delete
        modelBuilder.Entity<Project>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Employee>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<Company>().HasQueryFilter(c => !c.IsDeleted);
        modelBuilder.Entity<ProjectEmployee>().HasQueryFilter(c => !c.IsDeleted);
        
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
        
        
        #region Task

        modelBuilder.Entity<Task>()
            .HasOne(x => x.AuthorEmployee)
            .WithMany(x => x.Task)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Task>()
            .HasOne(x => x.ImplementorEmpoyee)
            .WithMany(x => x.Task)
            .HasForeignKey(x => x.ImplementorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Task>()
            .HasOne(x => x.Project)
            .WithMany(x => x.Task)
            .HasForeignKey(x => x.ProjectId)
            .OnDelete(DeleteBehavior.Restrict);

        #endregion
        
        
        base.OnModelCreating(modelBuilder);
    }
}