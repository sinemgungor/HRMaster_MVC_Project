using HRMaster_MVC_Project.Models.Enums;
using HRMaster_MVC_Project.Models.ViewModels;

public class UserDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string SecondName { get; set; }
    public string Surname { get; set; }
    public string SecondSurname { get; set; }
    public string PicturePath { get; set; }
    public DateOnly? BirthDate { get; set; }
    public string BirthState { get; set; }
    public string IdentityNumber { get; set; }
    public DateOnly? HireDate { get; set; }
    public DateOnly? TerminationDate { get; set; }
    public string Job { get; set; }
    public string Address { get; set; }
    public decimal? Salary { get; set; }
    public string Title { get; set; }
    public BloodGroup BloodGroup { get; set; }
    public Gender Gender { get; set; }
    public MarialStatus MarialStatus { get; set; }
    public int? EmployeeManagerID { get; set; }
    public string EmployeeManager { get; set; }
    public int? DepartmentID { get; set; }
    public int? CEORelatedCompanyID { get; set; }
    public int? EmployeeRelatedCompanyID { get; set; }
    public string UserName { get; set; }
    public string NormalizedUserName { get; set; }
    public string Email { get; set; }
    public string NormalizedEmail { get; set; }
    public bool EmailConfirmed { get; set; }
    public string PasswordHash { get; set; }
    public string SecurityStamp { get; set; }
    public string ConcurrencyStamp { get; set; }
    public string PhoneNumber { get; set; }
    public bool PhoneNumberConfirmed { get; set; }
    public bool TwoFactorEnabled { get; set; }
    public DateTimeOffset? LockoutEnd { get; set; }
    public bool LockoutEnabled { get; set; }
    public int AccessFailedCount { get; set; }
}
