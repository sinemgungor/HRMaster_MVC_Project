using HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.ViewModels
{
    public class AdvanceRequestPageVM
    {
        public AddAdvanceRequestDTO? AddAdvanceRequestDTO { get; set; }
        public List<ShowAdvanceRequestDTO>? AdvanceRequests { get; set; }
    }
}
