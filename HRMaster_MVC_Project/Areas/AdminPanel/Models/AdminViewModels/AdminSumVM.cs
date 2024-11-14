using HRMaster_MVC_Project.Models.ViewModels;

namespace HRMaster_MVC_Project.Areas.AdminPanel.Models.AdminViewModels
{
    public class AdminSumVM
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? PicturePath { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Job { get; set; }
        public string? Address { get; set; }
        public DepartmentVM? Department { get; set; }
    }
}
