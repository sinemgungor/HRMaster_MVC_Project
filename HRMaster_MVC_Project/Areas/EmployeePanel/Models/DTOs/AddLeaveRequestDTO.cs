using HRMaster_MVC_Project.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs
{
    public class AddLeaveRequestDTO
    {
        public LeaveType LeaveType { get; set; }

        [Required(ErrorMessage = "Başlangıç tarihi gereklidir.")]
        public DateOnly LeaveStartingDate { get; set; }

        [Required(ErrorMessage = "Bitiş tarihi gereklidir.")]      
        public DateOnly LeaveEndDate { get; set; }
        public double LeaveDays { get; set; }

        public int EmployeeID { get; set; }
    }
}
