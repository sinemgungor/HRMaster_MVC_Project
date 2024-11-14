using HRMaster_MVC_Project.Areas.EmployeePanel.Models.DTOs;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HRMaster_MVC_Project.Areas.CEOPanel.Models.CeoViewModel
{
    public class AssignDepartmentVM
    {
        public int EmployeeID { get; set; }
        public SelectList? Departments { get; set; }
        public int? SelectedDepartmentID { get; set; }
    }
}
