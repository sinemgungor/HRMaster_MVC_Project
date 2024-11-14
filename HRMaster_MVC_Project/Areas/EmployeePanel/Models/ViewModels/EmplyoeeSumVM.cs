using HRMaster_MVC_Project.Models.ViewModels;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.ViewModels
{
    public class EmplyoeeSumVM
    {

        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PicturePath { get; set; }
        public string? Email { get; set; }
        public string? Job { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }

        public DepartmentVM? Department { get; set; }
    }
}
