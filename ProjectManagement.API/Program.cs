using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProjectManagement.BLL.Interfaces;
using ProjectManagement.BLL.Services;
using ProjectManagement.DAL;
using ProjectManagement.DAL.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();




// Add DbContext
builder.Services.AddDbContext<ProjectManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ProjectManagementContext>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

builder.Services.Configure<IdentityOptions>(option =>
{    
    // Password settings, lockout settings, etc.
    
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequiredAdminRole", policy => policy.RequireRole("Admin"));
    
    options.AddPolicy("IsProjectManager", policy => policy.RequireRole("ProjectManager"));
    options.AddPolicy("ViewOwnProjects", policy => 
        policy.RequireRole("ProjectManager").RequireClaim("ProjectManagerId"));
    
    
    options.AddPolicy("IsEmployee", policy => policy.RequireRole("Employee"));

});




builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); 

// Add AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); 

// Add services for dependency injection
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();  // Important if you use Razor pages

app.Run();



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
