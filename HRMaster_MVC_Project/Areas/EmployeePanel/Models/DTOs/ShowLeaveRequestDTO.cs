using HRMaster_MVC_Project.Models.Enums;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs
{
    public class ShowLeaveRequestDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateOnly LeaveStartingDate { get; set; }
        public DateOnly LeaveEndDate { get; set; }
        public double LeaveDays { get; set; }
        public LeaveType LeaveType { get; set; }
        public ApprovalStatus ApprovalStatus { get; set; }
        public DateTime? ResponseDate { get; set; }
        public DateTime RequestDate { get; set; }
    }
}
