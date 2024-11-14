using HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.ViewModels
{
    public class LeaveRequestPageVM
    {
        public AddLeaveRequestDTO? AddLeaveRequestDTO { get; set; }
        public List<ShowLeaveRequestDTO>? LeaveRequests { get; set; }
    }
}
